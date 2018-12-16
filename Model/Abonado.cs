namespace Model
{
    public class Abonado
    {
        public virtual int Id { get; set; }
        public virtual string Number { get; set; }
        public virtual string Name { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual string Address { get; set; }
    }
}
