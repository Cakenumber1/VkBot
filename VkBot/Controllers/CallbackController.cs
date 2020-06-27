using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VkNet.Model;
using VkNet.Utils;
using VkNet.Model.RequestParams;
using VkNet.Abstractions;
using System;

namespace VkBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        /// <summary>
        /// Конфигурация приложения
        /// </summary>

        private readonly IConfiguration _configuration;

        private readonly IVkApi _vkApi;

        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Callback([FromBody] Updates updates)
        {
            // Тип события
            switch (updates.Type)
            {
                // Ключ-подтверждение
                case "confirmation":
                    {
                        return Ok(_configuration["Config:Confirmation"]);
                    }

                // Новое сообщение
                case "message_new":
                    {
                        Tuple<string, string>[] a = new Tuple<string, string>[6];
                        a[0] = new Tuple<string, string>("CHF", "Франк");
                        a[1] = new Tuple<string, string>("JPY", "Йена");
                        a[2] = new Tuple<string, string>("EUR", "Евро");
                        a[3] = new Tuple<string, string>("GBP", "Фунт");
                        a[4] = new Tuple<string, string>("USD", "Доллар");
                        a[5] = new Tuple<string, string>("CNY", "Юань");
                        Search s = new Search(a);
                        // Десериализация
                        var msg = Message.FromJson(new VkResponse(updates.Object));
                        string test = msg.Text;
                        s.searchSPB(test);
                        s.searchOth(test);
                        string send = s.printResult();
                        //ответ
                        _vkApi.Messages.Send(new MessagesSendParams
                        {
                            //RandomId = new DateTime().Second,
                            PeerId = msg.PeerId.Value,
                            Message = send
                        });
                        break;
                    }
            }

            return Ok("ok");
        }
    }
}
