using System;
using Factory;
using Graph;
using Settable;

namespace Presenters
{
    public class BaseGridNodeModel : BaseModel, ISettable
    {
        public GridNode GridNode { get; private set; }
        
        public void Setup(AbstractSetupConfig setupConfig)
        {
            BaseGridNodeModelSetupConfig config = setupConfig as BaseGridNodeModelSetupConfig;

            GridNode = config.GridNode;
        }
        
        internal class BaseGridNodeModelSetupConfig : AbstractSetupConfig
        {
            public GridNode GridNode;
        }
    }
}