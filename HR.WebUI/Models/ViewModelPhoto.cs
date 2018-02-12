

using HR.DateLayer.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebUI.Models
{
    public class ViewModelPhoto
    {
        public Employee employee { get; set; }
        public string JsonPhotoString { get; set; }
        public string PreviewPhotoString { get; set; }
        //PreviewPhotoString
    }
}
