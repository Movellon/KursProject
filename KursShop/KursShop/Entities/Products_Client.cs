//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KursShop.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products_Client
    {
        public int id_product { get; set; }
        public Nullable<int> quantity { get; set; }
        public string nameProduct { get; set; }
        public string id_clients { get; set; }
        public byte[] imageData { get; set; }
    }
}