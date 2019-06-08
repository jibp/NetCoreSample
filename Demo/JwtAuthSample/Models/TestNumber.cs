using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthSample.Models
{
    public class TestNumber
    {
        [Required(ErrorMessage = "项目阶段名称必填")]
        public string name { get; set; }

        [Required(ErrorMessage = "排序号必须为数字")]
        public int sort { get; set; }
    }
}
