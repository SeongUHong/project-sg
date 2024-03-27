using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore
{
    // 작업들의 집합
    // 같은 류의 작업들을 아우르는 클래스는 본 인터페이스를 계승해야함
    public interface IJobQueue
    {
        void Push(Action job);
    }
}
