using MitMediator.AutoApi.Abstractions;

// Black magic that makes the field invisible to all mappings, but still accessible in code.

namespace MitMediator
{
    public static class KeyRequestExtensions
    {  
        extension<TKey>(IKeyRequest<TKey> keyRequest)
        {
            public TKey Key
            {
                get => keyRequest.GetKey();
                set => keyRequest.SetKey(value);
            }
        }
        
        extension<TKey1, TKey2>(IKeyRequest<TKey1, TKey2> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
        }
        
        extension<TKey1, TKey2, TKey3>(IKeyRequest<TKey1, TKey2, TKey3> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
            
            public TKey3 Key3
            {
                get => keyRequest.GetKey3();
                set => keyRequest.SetKey3(value);
            }
        }
        
        extension<TKey1, TKey2, TKey3, TKey4>(IKeyRequest<TKey1, TKey2, TKey3, TKey4> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
            
            public TKey3 Key3
            {
                get => keyRequest.GetKey3();
                set => keyRequest.SetKey3(value);
            }
            
            public TKey4 Key4
            {
                get => keyRequest.GetKey4();
                set => keyRequest.SetKey4(value);
            }
        }
        
        extension<TKey1, TKey2, TKey3, TKey4, TKey5>(IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
            
            public TKey3 Key3
            {
                get => keyRequest.GetKey3();
                set => keyRequest.SetKey3(value);
            }
            
            public TKey4 Key4
            {
                get => keyRequest.GetKey4();
                set => keyRequest.SetKey4(value);
            }
            
            public TKey5 Key5
            {
                get => keyRequest.GetKey5();
                set => keyRequest.SetKey5(value);
            }
        }
        
        extension<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>(IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
            
            public TKey3 Key3
            {
                get => keyRequest.GetKey3();
                set => keyRequest.SetKey3(value);
            }
            
            public TKey4 Key4
            {
                get => keyRequest.GetKey4();
                set => keyRequest.SetKey4(value);
            }
            
            public TKey5 Key5
            {
                get => keyRequest.GetKey5();
                set => keyRequest.SetKey5(value);
            }
            
            public TKey6 Key6
            {
                get => keyRequest.GetKey6();
                set => keyRequest.SetKey6(value);
            }
        }
        
        extension<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>(IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
            
            public TKey3 Key3
            {
                get => keyRequest.GetKey3();
                set => keyRequest.SetKey3(value);
            }
            
            public TKey4 Key4
            {
                get => keyRequest.GetKey4();
                set => keyRequest.SetKey4(value);
            }
            
            public TKey5 Key5
            {
                get => keyRequest.GetKey5();
                set => keyRequest.SetKey5(value);
            }
            
            public TKey6 Key6
            {
                get => keyRequest.GetKey6();
                set => keyRequest.SetKey6(value);
            }
            
            public TKey7 Key7
            {
                get => keyRequest.GetKey7();
                set => keyRequest.SetKey7(value);
            }
        }
    }
}

namespace MitMediator.Tasks
{
    public static class KeyRequestExtensions
    {  
        extension<TKey>(IKeyRequest<TKey> keyRequest)
        {
            public TKey Key
            {
                get => keyRequest.GetKey();
                set => keyRequest.SetKey(value);
            }
        }
        
        extension<TKey1, TKey2>(IKeyRequest<TKey1, TKey2> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
        }
        
        extension<TKey1, TKey2, TKey3>(IKeyRequest<TKey1, TKey2, TKey3> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
            
            public TKey3 Key3
            {
                get => keyRequest.GetKey3();
                set => keyRequest.SetKey3(value);
            }
        }
        
        extension<TKey1, TKey2, TKey3, TKey4>(IKeyRequest<TKey1, TKey2, TKey3, TKey4> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
            
            public TKey3 Key3
            {
                get => keyRequest.GetKey3();
                set => keyRequest.SetKey3(value);
            }
            
            public TKey4 Key4
            {
                get => keyRequest.GetKey4();
                set => keyRequest.SetKey4(value);
            }
        }
        
        extension<TKey1, TKey2, TKey3, TKey4, TKey5>(IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
            
            public TKey3 Key3
            {
                get => keyRequest.GetKey3();
                set => keyRequest.SetKey3(value);
            }
            
            public TKey4 Key4
            {
                get => keyRequest.GetKey4();
                set => keyRequest.SetKey4(value);
            }
            
            public TKey5 Key5
            {
                get => keyRequest.GetKey5();
                set => keyRequest.SetKey5(value);
            }
        }
        
        extension<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>(IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
            
            public TKey3 Key3
            {
                get => keyRequest.GetKey3();
                set => keyRequest.SetKey3(value);
            }
            
            public TKey4 Key4
            {
                get => keyRequest.GetKey4();
                set => keyRequest.SetKey4(value);
            }
            
            public TKey5 Key5
            {
                get => keyRequest.GetKey5();
                set => keyRequest.SetKey5(value);
            }
            
            public TKey6 Key6
            {
                get => keyRequest.GetKey6();
                set => keyRequest.SetKey6(value);
            }
        }
        
        extension<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>(IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7> keyRequest)
        {
            public TKey1 Key1
            {
                get => keyRequest.GetKey1();
                set => keyRequest.SetKey1(value);
            }
            
            public TKey2 Key2
            {
                get => keyRequest.GetKey2();
                set => keyRequest.SetKey2(value);
            }
            
            public TKey3 Key3
            {
                get => keyRequest.GetKey3();
                set => keyRequest.SetKey3(value);
            }
            
            public TKey4 Key4
            {
                get => keyRequest.GetKey4();
                set => keyRequest.SetKey4(value);
            }
            
            public TKey5 Key5
            {
                get => keyRequest.GetKey5();
                set => keyRequest.SetKey5(value);
            }
            
            public TKey6 Key6
            {
                get => keyRequest.GetKey6();
                set => keyRequest.SetKey6(value);
            }
            
            public TKey7 Key7
            {
                get => keyRequest.GetKey7();
                set => keyRequest.SetKey7(value);
            }
        }
    }
}

