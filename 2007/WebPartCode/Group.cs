namespace BilSimser.SharePointForums.WebPartCode
{
    public class Group : DomainObject
    {
        public Group()
        {
        }

        public Group(string name)
        {
            Name = name;
        }

        public Group(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}