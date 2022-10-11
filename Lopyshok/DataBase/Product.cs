//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lopyshok.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ProductMaterial = new HashSet<ProductMaterial>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> MinPrice { get; set; }
        public Nullable<int> ProductTypeId { get; set; }
        public Nullable<int> ManForProduction { get; set; }
        public Nullable<int> WorkshopId { get; set; }
        public byte[] Image { get; set; }
    
        public virtual ProductType ProductType { get; set; }
        public virtual Workshop Workshop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductMaterial> ProductMaterial { get; set; }
    }
}
