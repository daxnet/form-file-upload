using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormFileUpload.Models
{
    public class SavedFile : IEntity
    {
        public DateTime Timestamp { get; set; }

        public string FileName { get; set; }

        public Guid Id { get; set; }
    }
}
