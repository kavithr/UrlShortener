//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UrlShortner.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UrlDetail
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string TinyUrl { get; set; }
        public string UrlHash { get; set; }
    }
}
