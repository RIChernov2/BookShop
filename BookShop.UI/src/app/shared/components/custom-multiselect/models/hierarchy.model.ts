export enum NodeState {
    None = 0,
    Unchecked = 1,
    Semichecked = 2,
    Checked = 3
}

export class DropdownNode {
    public data?: any;
    public children: DropdownNode[] = [];
    public parent?: DropdownNode;
    public hidden?: boolean;
    private _state: NodeState = NodeState.None;

    constructor(init?: Partial<DropdownNode>) {
        Object.assign(this, init);
    }

    public get state(){
        return this._state;
    }

    public set state(newState: NodeState){
        if (this._state === newState)
            return;

        if (newState === NodeState.None){
            this._state = newState;
            return;
        }

        this._state = newState;

        if (newState !== NodeState.Semichecked) {
            this.children?.forEach( child => child.state = newState );
        }

        if (this.parent) {
            this.parent.state = this.parent.children.every( child => child.state === newState ) ?
                newState :
                NodeState.Semichecked;
        }
    }

}

export class Hierarchy {
    public nodes?: DropdownNode[];
    public selected: any[] = [];
    private availableForSelect: DropdownNode[] = []; // nodes without children

    constructor(data: any[], private idField: string, private valueField: string,
                private parentIdField: string, private hiddenAttrField: string, private allDataSelectedByDefault: boolean) {
        this.createNodes(data);
    }

    public updateAvailable(data: any[], callback: Function): void {
        this.createNodes(data);
        this.selected = this.selected.filter(elem =>
            this.availableForSelect.some(node => node.data[this.idField] === elem[this.idField])
        );
        callback(this.selected);
    }

    public updateSelected(nextSelected: any[], onError: Function): void {
        const filtered: any[] = [];

        // if 'allDataSelectedByDefault' param applied, nextSelected == null | undefined or empty array
        // is equal to all nodes selected
        if (nextSelected?.length === 0 && this.allDataSelectedByDefault) {
            this.availableForSelect.forEach( node => node.state = NodeState.Checked );
            this.selected = filtered;
            return;
        }

        // otherwise all nodes will be unchecked
        if (nextSelected == null || nextSelected.length === 0) {
            this.availableForSelect.forEach( node => node.state = NodeState.Unchecked );
            this.selected = filtered;
            return;
        }

        // if nextSelected.length > 0, find node with same id and mark it checked/unchecked, also count number of checked nodes
        this.availableForSelect.forEach( node => {
            node.state = nextSelected.find( elem => elem[this.idField] === node.data[this.idField] ) ? NodeState.Checked : NodeState.Unchecked;
            if (node.state === NodeState.Checked) filtered.push(node.data);
        });

        // if nextSelected.length > 0, but number of checked nodes is 0, show error
        if (nextSelected.length > 0 && filtered.length === 0) {
            const availableData = this.availableForSelect.map(node => `id: ${node.data[this.idField]}, name: ${node.data[this.valueField]}`).join(',\n');
            onError('Selected nodes are not permitted.');
            throw new Error(`Nodes ${JSON.stringify(nextSelected)} are not available.\nAvailable nodes:\n${availableData}.`);
        }

        this.selected = this.allDataSelectedByDefault && filtered.length  === this.availableForSelect.length ? [] : filtered;
    }

    public getChecked(): any[] {
        const selected = this.availableForSelect.filter( node => node.state === NodeState.Checked );
        if (this.allDataSelectedByDefault && selected.length === this.availableForSelect.length) {
            this.selected = [];
        } else {
            this.selected = selected.map( x => x.data );
        }
        return this.selected;
    }

    private createNodes(data: any[]): void {
        const allNodes = data.sort( (el1, el2) => el1.sortOrder - el2.sortOrder ).map( elem => new DropdownNode({
            data: elem,
            hidden: !!elem[this.hiddenAttrField]
        }));

        if (this.parentIdField) {
            allNodes.forEach( node => {
                node.children = allNodes.filter( x => x.data[this.parentIdField] === node.data[this.idField] );
                node.parent = allNodes.find( x => x.data[this.idField] === node.data[this.parentIdField] );
            });
            this.nodes = allNodes.filter( x => x.parent == null );
            this.availableForSelect = allNodes.filter( x => x.children.length === 0 );
        } else {
            this.nodes = this.availableForSelect = allNodes;
        }
    }

}
