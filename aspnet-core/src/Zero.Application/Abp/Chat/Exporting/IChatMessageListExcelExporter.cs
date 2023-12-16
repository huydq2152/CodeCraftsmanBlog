using System.Collections.Generic;
using Abp;
using Zero.Chat.Dto;
using Zero.Dto;

namespace Zero.Abp.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
