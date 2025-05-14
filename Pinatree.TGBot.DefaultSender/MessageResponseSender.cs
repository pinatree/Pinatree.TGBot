using Pinatree.TGBot.ISender;
using System.Collections;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pinatree.TGBot.DefaultSender
{
    public sealed class MessageResponseSender : IMessageResponseSender
    {
        ITelegramBotClient _botClient;

        public MessageResponseSender(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task SendMessage(long chatId, string message)
        {
            await _botClient.SendTextMessageAsync(chatId, message);
        }

        public async Task SendMessageWithScreenKeyboard(long chatId, string message, IEnumerable<IEnumerable<string>> btns)
        {
            IEnumerable<IEnumerable<KeyboardButton>> availableKeys = btns.Select(
                (str) =>
                {
                    return str.Select(
                        (key) =>
                        {
                            return new KeyboardButton(key);
                        }
                    );
                }
            );

            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(availableKeys);

            await _botClient.SendTextMessageAsync(chatId, message, replyMarkup: keyboard);
        }
    }
}
