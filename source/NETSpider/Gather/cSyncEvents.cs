﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NETSpider.Gather
{
    public class cSyncEvents
    {
        public cSyncEvents()
        {
            // AutoResetEvent 用于“新项”事件，因为  
            // 我们希望每当使用者线程响应此事件时，  
            // 此事件就会自动重置。  
            _newItemEvent = new AutoResetEvent(false);

            // ManualResetEvent 用于“退出”事件，因为  
            // 我们希望发出此事件的信号时有多个线程响应。  
            // 如果使用 AutoResetEvent，事件  
            // 对象将在单个线程作出响应之后恢复为   
            // 未发信号的状态，而其他线程将  
            // 无法终止。  
            _exitThreadEvent = new ManualResetEvent(false);

            // 这两个事件也放在一个 WaitHandle 数组中，以便  
            // 使用者线程可以使用 WaitAny 方法  
            // 阻塞这两个事件。  
            _eventArray = new WaitHandle[2];
            _eventArray[0] = _newItemEvent;
            _eventArray[1] = _exitThreadEvent;  
        }
        public EventWaitHandle ExitThreadEvent
        {
            get { return _exitThreadEvent; }
        }
        public EventWaitHandle NewItemEvent
        {
            get { return _newItemEvent; }
        }
        public WaitHandle[] EventArray
        {
            get { return _eventArray; }
        }
        private EventWaitHandle _newItemEvent;
        private EventWaitHandle _exitThreadEvent;
        private WaitHandle[] _eventArray;  
    }
}
