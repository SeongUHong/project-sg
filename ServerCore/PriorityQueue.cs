using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        // NOTE
        // 우선순위 큐
        // 노드 구조에서 부모 자식 간의 크기를 비교해서
        // 자식이 큰 경우 부모와 자식을 교체

        List<T> _heap = new List<T>();

        public int Count { get { return _heap.Count; } }

        // 데이터 추가
        public void Push(T data)
        {
            // 힙의 맨 끝에 새로운 데이터를 삽입
            _heap.Add(data);

            // 크기에 따른 인덱스에 배치
            int now = _heap.Count - 1;
            while (now > 0)
            {
                // 부모 노드와 비교
                int parent = (now - 1) / 2;
                if (_heap[now].CompareTo(_heap[parent]) < 0)
                    break;

                // 부모 노드와 위치 교환
                T temp = _heap[now];
                _heap[now] = _heap[parent];
                _heap[parent] = temp;

                // 비교 위치 교체
                now = parent;
            }
        }

        // 루트(1순위) 값을 반환 후 우선순위를 재조정함
        public T Pop()
        {
            // 반환할 데이터 보존
            T ret = _heap[0];

            // 마지막 데이터를 루트로 이동
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;

            // 우선 순위 노드 재조정
            int now = 0;
            while (true)
            {
                // 왼쪽 노드
                int left = now * 2 + 1;
                // 오른쪽 노드
                int right = now * 2 + 2;

                int next = now;
                // 왼쪽값이 현재값보다 크면 왼쪽으로 이동
                if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                    next = left;
                // 오른쪽값이 현재값(왼쪽값 포함)보다 크면 오른쪽으로 이동
                if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                    next = right;

                // 왼쪽, 오른쪽 모두 현재값보다 작으면 종료
                if (next == now)
                    break;

                // 자식 노드와 위치 교환
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // 비교 위치 교체
                now = next;
            }

            return ret;
        }

        // 루트 값을 참조만 함
        public T Peek()
        {
            if (_heap.Count < 0)
                return default(T);
            return _heap[0];
        }
    }
}
