using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Models
{
    public class Page
    {
        private bool _initialized;

        public void Init()
        {
            if (_initialized)
            {
                return;
            }
            //find all label's template content
            Template.Init();

            _initialized = true;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SiteId { get; set; }

        public int TemplateId { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Keywords { get; set; }

        public string Description { get; set; }

        public string UrlPattern { get; set; }

        [ForeignKey("TemplateId")]
        public Template Template { get; set; }

        [NotMapped]
        public OutputCache OutputCache { get; set; }

        [NotMapped]
        public UrlRoute UrlRoute { get; set; }

        [NotMapped]
        public RouteData RouteData { get; set; }

        public string GetReplacedContext()
        {
            return Template.GetReplacedContent();
        }
    }
}
