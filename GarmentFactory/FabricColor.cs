//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GarmentFactory
{
    using System;
    using System.Collections.Generic;
    
    public partial class FabricColor
    {
        public int IdFabricColor { get; set; }
        public string IdFabric { get; set; }
        public int IdColor { get; set; }
    
        public virtual Color Color { get; set; }
        public virtual Fabric Fabric { get; set; }
    }
}
