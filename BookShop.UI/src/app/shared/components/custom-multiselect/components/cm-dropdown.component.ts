import { Component, Input, Output, EventEmitter,
         HostListener, ElementRef, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { DropdownNode, Hierarchy, NodeState } from '../models/hierarchy.model';

@Component({
  selector: 'cm-dropdown',
  templateUrl: './cm-dropdown.component.html',
  styleUrls: ['./cm-dropdown.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CmDropdownComponent {
  @Input() hierarchy?: Hierarchy;
  @Input() valueField?: string;
  @Input() idField?: string;
  @Input() dropdownOnTop?: boolean;
  @Input() parentRef?: HTMLDivElement;

  @Output() dropdownCloseEvent = new EventEmitter();
  @Output() selectedChanges = new EventEmitter<DropdownNode>();

  public nodeState = NodeState;

  @HostListener('document:click', ['$event'])
  clickout(event: MouseEvent) {
    if ( !this.parentRef?.contains(event.target as Node) && !this.elRef.nativeElement.contains(event.target) ) {
      this.dropdownCloseEvent.next(null);
    }
  }

  constructor(private elRef: ElementRef, private cdRef: ChangeDetectorRef) { }

  public toggleNode(node: DropdownNode): void {
    setTimeout( () => {
      node.hidden = !node.hidden;
      this.cdRef.detectChanges();
    } );
  }

  public onCheckboxChange(node: DropdownNode): void {
    setTimeout( () => {
      node.state = node.state !== NodeState.Checked ? NodeState.Checked : NodeState.Unchecked;
      this.selectedChanges.next(node);
    });
  }

}
