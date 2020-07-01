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

            switch (updates.Type)
            {

                case "confirmation":
                    {
                        return Ok(_configuration["Config:Confirmation"]);
                    }

                case "message_new":
                    { 
                        Search s = new Search();
                        var msg = Message.FromJson(new VkResponse(updates.Object));
                        if (!msg.Text.IsNullOrEmpty())
                        {
                            string test;
                            if (msg.Text.Contains("@botnumbernotone"))
                            {
                                test = msg.Text.Substring(33, msg.Text.Length - 33);
                            }
                            else
                            {
                                test = msg.Text;
                            }
                            test = s.logsCall(test);


                            _vkApi.Messages.Send(new MessagesSendParams
                            {
                                RandomId = new DateTime().Millisecond,
                                PeerId = msg.PeerId.Value,
                                Message = test
                            });
                            if (test == "!stonks")
                            {

                                var albumid = 273064435;
                                var photos = _vkApi.Photo.Get(new PhotoGetParams
                                {
                                    AlbumId = PhotoAlbumType.Id(albumid),
                                    OwnerId = 196633398
                                });
                                _vkApi.Messages.Send(new MessagesSendParams
                                {
                                    Attachments = photos,
                                    Message = "Message",
                                    PeerId = _vkApi.UserId.Value
                                });
                            }

                            s.searchOth(test);
                            string send = s.printResult();
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
                                //Are you dumb, stupid, or dumb?
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
