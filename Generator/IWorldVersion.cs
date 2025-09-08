using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Generator;

//source: net.minecraft.WorldVersion
public interface IWorldVersion
{
    //DataVersion dataVersion();

    string id();

    string name();

    int protocolVersion();

    //int packVersion(PackType p_405842_);

    DateTime buildTime();

    bool stable();
}
