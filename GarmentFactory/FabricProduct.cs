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
    
    public partial class FabricProduct
    {
        public string IdFabric { get; set; }
        public string IdProduct { get; set; }
        public int Count { get; set; }
    
        public virtual Fabric Fabric { get; set; }
        public virtual Product Product { get; set; }
    }
}
