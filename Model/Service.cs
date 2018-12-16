using System;

namespace Model
{
    public class Service
    {
        public virtual int Id { get; set; }
        public virtual Abonado Abonado { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Issue { get;set; }
    }
}
