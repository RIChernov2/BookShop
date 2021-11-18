using BookShop.Notifications.Manager.Models;
using Dapper;
using System;
using System.Data;

namespace BookShop.Notifications.Manager.Repositories.Mappers
{
    public class NSettingsTypeHandler : SqlMapper.TypeHandler<NSettings>
    {
        public override NSettings Parse(object value)
        {
            return NSettings.FromString(value.ToString());
        }

        // Handles how data is saved into the database
        public override void SetValue(IDbDataParameter parameter, NSettings value)
        {
            parameter.Value = value.ToString();
        }
    }
}
