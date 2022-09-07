using BlogProjem.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Model.Entities.Abstract
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }

        private DateTime _createdDate = DateTime.Now;
        public DateTime CreatedDate 
        {
            get { return _createdDate; }
            set { _createdDate = value; } 
        }

        public DateTime? ModifiedDate { get; set; }  // ? => nullable 

        public DateTime? RemovedDate { get; set; }

        private Statu _status = Statu.Passive; //aktifden pasife çektik

        public Statu Statu
        {
            get { return _status; }
            set { _status = value; }
        }

    }
}
