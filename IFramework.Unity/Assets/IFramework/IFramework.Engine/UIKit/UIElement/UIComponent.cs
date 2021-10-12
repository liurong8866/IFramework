using IFramework.Core;

namespace IFramework.Engine
{
    public abstract class UIComponent : UIElement
    {
        public override BindType BindType => BindType.Component;
    }
}
