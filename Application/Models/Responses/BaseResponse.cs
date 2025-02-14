using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.Responses {
    public class BaseResponse <T>{
        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public HashSet<string>? Errors { get; set; } = null;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Result { get; set; } = default;

    }
}
