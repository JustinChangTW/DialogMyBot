using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DialogMyBot.Dialogs
{
    public class UserProfileDialog:ComponentDialog
    {
        private WaterfallStep[] _waterfallSteps;

        public UserProfileDialog() : base(nameof(UserProfileDialog))
        {
            _waterfallSteps = new WaterfallStep[]
            {
                NameStepSync,
                SummaryStepAsync,
            };

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), _waterfallSteps));
            AddDialog(new TextPrompt(nameof(TextPrompt)));

        }

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var result = (string)stepContext.Result??"";

            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"您輸入的姓名是.....{result}"),cancellationToken);

            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

        private async Task<DialogTurnResult> NameStepSync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            PromptOptions promptOptions = new PromptOptions()
            {
                Prompt=MessageFactory.Text("請輸入姓名....")
            };

            return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);
        }


    }
}
