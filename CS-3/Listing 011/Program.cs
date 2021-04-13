// Use Parallel.For() to create a data-parallel loop. 

using System;
using System.Threading;
using System.Threading.Tasks;

class DemoParallelFor
{
    static int[] data;

    // A method to be run as the body of a parallel loop. 
    // The statements in this loop are designed to simply 
    // consume some CPU time for the puposes of demonstration. 
    static void MyTransform(int i)
    {
        // So something for demonstration purposes. 

        data[i] = data[i] / 10;

        if (data[i] < 10000) data[i] = 0;
        if (data[i] > 10000 & data[i] < 20000) data[i] = 100;
        if (data[i] > 20000 & data[i] < 30000) data[i] = 200;
        if (data[i] > 30000) data[i] = 300;
        //Console.WriteLine(Thread);
    }

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        data = new int[100000000];

        // Initialize the data using a regular for loop. 
        for (int i = 0; i < data.Length; i++) data[i] = i;

        // A parallel For loop. 
        Parallel.For(0, data.Length, MyTransform);

        Console.WriteLine("Main thread ending.");

        Console.Read();

    }
}
/*
В TPL параллелизм данных поддерживается, в частности, с помощью метода For (), определенного в классе Parallel. Этот метод существует в нескольких формах. Его рассмотрение мы начнем с самой простой формы, приведенной ниже:
public static ParallelLoopResult
For(int fromlnclusive, int toExclusive, Action<int> body)
где fromlnclusive обозначает начальное значение того, что соответствует переменной управления циклом; оно называется также итерационным, или индексным, значением; a toExclusive — значение, на единицу больше конечного. На каждом шаге цикла переменная управления циклом увеличивается на единицу. Следовательно, цикл постепенно продвигается от начального значения fromlnclusive к конечному значению toExclusive минус единица. Циклически выполняемый код указывается методом, передаваемым через параметр body. Этот метод должен быть совместим с делегатом Action<int>, объявляемым следующим образом.
public delegate void Actioncin T>(T obj)
Для метода For () обобщенный параметр Т должен быть, конечно, типа int. Значение, передаваемое через параметр obj, будет следующим значением переменной управления циклом. А метод, передаваемый через параметр body, может быть именованным или анонимным. Метод For () возвращает экземпляр объекта типа ParallelLoopResult, описывающий состояние завершения цикла. Для простых циклов этим значением можно пренебречь. (Более подробно это значение будет рассмотрено несколько ниже.)
Главная особенность метода For () состоит в том, что он позволяет, когда такая возможность имеется, распараллелить исполнение кода в цикле. А это, в свою очередь, может привести к повышению производительности. Например, процесс преобразования массива в цикле может быть разделен на части таким образом, чтобы разные части массива преобразовывались одновременно. Следует, однако, иметь в виду, что повышение производительности не гарантируется из-за отличий в количестве доступных процессоров в разных средах выполнения, а также из-за того, что распараллеливание мелких циклов может составить издержки, которые превышают сэкономленное время.
*/
