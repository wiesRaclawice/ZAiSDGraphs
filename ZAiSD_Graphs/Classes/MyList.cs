using System.CodeDom;
using System.Collections.Generic;

namespace ZAiSD_Graphs.Classes
{
    public class MyList<T>
    {
        public int Length { get; set; }

        private class Element
        {
            private Element next;
            private T data;

            public Element(T t)
            {
                next = null;
                data = t;
            }

            public Element Next
            {
                get { return next; }
                set { next = value; }
            }

            public T Data
            {
                get { return data; }
                set { data = value; }
            }
        }

        private Element head;
        private Element tail;

        public MyList()
        {
            head = null;
            tail = null;
            Length = 0;
        }

        public void Add(T t)
        {
            Element e = new Element(t);
            if (head == null)
            {
                head = tail = e;
            }
            else
            {
                tail.Next = e;
                tail = e;
            }
            Length += 1;
        }

        public void Remove(T t)
        {
            if (head != null)
            {
                Element e = head;
                Element previous = head;
                while (e != null)
                {
                    if (e.Data.Equals(t))
                    {
                        if (e == head)
                        {
                            head = e.Next;
                            break;
                        } 
                        
                        if (e == tail)
                        {
                            previous.Next = null;
                            tail = previous;
                            break;
                        }
                        
                        previous.Next = e.Next;
                        break;
                    }

                    previous = e;
                    e = e.Next;
                    
                }
            }
            Length -= 1;
        }

        public bool Contains(T t)
        {
            var element = head;
            var found = false;

            while (element != null)
            {
                if (element.Data.Equals(t))
                {
                    found = true;
                    break;
                }
                element = element.Next;
            }
            return found;
        }

        

        public IEnumerator<T> GetEnumerator()
        {
            Element current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

    }
}