using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VkNet.Model;
using VkNet.Utils;
using VkNet.Model.RequestParams;
using VkNet.Abstractions;
using System;
using VkNet.Model.Attachments;
using VkNet.Enums.SafetyEnums;
using CsQuery.ExtensionMethods.Internal;

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

                        Search s = new Search();
                        // Десериализация
                        var msg = Message.FromJson(new VkResponse(updates.Object));
                        if (!msg.Text.IsNullOrEmpty())
                        {
                            string test;
                            if (msg.Text.Contains("@botnumbernotone"))
                            {
                                test = msg.Text.ToLower();
                            }
                            else
                            {
                                test = msg.Text.ToUpper();
                            }

                            _vkApi.Messages.Send(new MessagesSendParams
                            {
                                RandomId = new DateTime().Minute,
                                PeerId = msg.PeerId.Value,
                                Message = test
                            });

                            s.searchOth(test);
                            string send = s.printResult();
                            //ответ
                            _vkApi.Messages.Send(new MessagesSendParams
                            {
                                RandomId = new DateTime().Millisecond,
                                PeerId = msg.PeerId.Value,
                                Message = send
                            });
                            break;
                        }
                        else
                        {
                            _vkApi.Messages.Send(new MessagesSendParams
                            {
                                RandomId = new DateTime().Millisecond,
                                PeerId = msg.PeerId.Value,
                                Message = "Чтобы получить справку по командам напишите \"!help\""
                        });
                            break;
                        }
                    

                    }
            }

            return Ok("ok");
        }
    }
}
