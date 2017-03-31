using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Limlzy.VerificationCode
{
    //=========================================================================
    // *  作者：   杨泉耀
    // *  时间：   2017/3/1 12:02:30
    // *  文件名： Enum      
    // *  版本：   1.0
    // *  说明：    
    //=========================================================================

    [Flags]
    public enum CharSet
    {
        China,
        English,
        Number
    }
    public enum Confusionlevel
    {
        Low,
        Middle,
        Height
    }
    
}
