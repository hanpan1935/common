﻿using Volo.Abp.Settings;

namespace Lanpuda.Server.Settings;

public class ServerSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ServerSettings.MySetting1));
    }
}
