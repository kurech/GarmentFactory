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
    
    public partial class WarehouseFabric
    {
        public string IdFabric { get; set; }
        public string Roll { get; set; }
        public int Length { get; set; }
    
        public virtual Fabric Fabric { get; set; }
    }
}
