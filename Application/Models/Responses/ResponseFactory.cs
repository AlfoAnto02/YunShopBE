using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses {
    public class ResponseFactory {
        public static BaseResponse<T> WithSuccess<T>(T result) {
            return new BaseResponse<T> {
                Result = result,
                Success = true
            };
        }

        public static BaseResponse<string> WithError(Exception exception) {
            return new BaseResponse<string> {
                Errors = new HashSet<string> { exception.Message },
                Success = false
            };
        }
    }
}
