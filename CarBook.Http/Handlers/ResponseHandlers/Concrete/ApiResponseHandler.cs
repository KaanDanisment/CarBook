using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Adapters.Handlers.ResponseHandlers.Concrete
{
    public class ApiResponseHandler : IApiResponseHandler
    {
        public async Task<IDataResult<T>> HandleApiResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                switch ((int)response.StatusCode)
                {
                    case 400:
                        return new ErrorDataResult<T>("Yanlış veya eksik bilgi girdiniz.Lütfen kontrol edin.", response.StatusCode.ToString());
                    case 401:
                        return new ErrorDataResult<T>("Lütfen giriş yapın.", response.StatusCode.ToString());
                    case 403:
                        return new ErrorDataResult<T>("Bu işlem için yetkiniz yok.", response.StatusCode.ToString());
                    case 404:
                        return new ErrorDataResult<T>("Aradığınız sayfa bulunamadı", response.StatusCode.ToString());
                    case 500:
                        return new ErrorDataResult<T>("Sunucu hatası.Lütfen daha sonra tekrar deneyiniz", response.StatusCode.ToString());
                    default:
                        return new ErrorDataResult<T>("Beklenmeyen bir hata oluştu.Lütfen daha sonra tekrar deneyin.", response.StatusCode.ToString());
                }
            }

            try
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(jsonData);
                return new SuccessDataResult<T>(data);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<T>(ex.Message);
            }
            
        }

        public async Task<IResult> HandleApiResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                switch ((int)response.StatusCode)
                {
                    case 400:
                        return new ErrorResult("Yanlış veya eksik bilgi girdiniz.Lütfen kontrol edin.", response.StatusCode.ToString());
                    case 401:
                        return new ErrorResult("Lütfen giriş yapın.", response.StatusCode.ToString());
                    case 403:
                        return new ErrorResult("Bu işlem için yetkiniz yok.", response.StatusCode.ToString());
                    case 404:
                        return new ErrorResult("Aradığınız sayfa bulunamadı", response.StatusCode.ToString());
                    case 500:
                        return new ErrorResult("Sunucu hatası.Lütfen daha sonra tekrar deneyiniz", response.StatusCode.ToString());
                    default:
                        return new ErrorResult("Beklenmeyen bir hata oluştu.Lütfen daha sonra tekrar deneyin.", response.StatusCode.ToString());
                }

            }
            return new SuccessResult();
        }
    }
}
