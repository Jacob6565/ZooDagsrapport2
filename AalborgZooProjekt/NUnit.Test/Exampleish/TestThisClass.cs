using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Test
{
    public class TestThisClass
    {
        /* The class after first test

        private int _number = 0;
        public int GetNumer => _number;


        public void AddNumber(int number)
        {
        }

        */

        /* the class after second test 
        private int _number = 0;
        public int GetNumer => _number;


        public void AddNumber(int number)
        {
            _number += number;
        }
        */

        /* After third test */
        INumberAdder _adder;
        public TestThisClass(INumberAdder adderStrategy)
        {
            _adder = adderStrategy;
        }

        private int _number = 0;
        public int GetNumer => _number;


        public void AddNumber(int number)
        {
            _number += _adder.AddNumber(number);
        }
    }

    /* To make it easier to change how to add number an interface has been made
     * it allows to swap the adderstrategy without changing the code much */


    public interface INumberAdder
    {
        int AddNumber(int number);
    }

    public class Adder1For1 : INumberAdder
    {
        public int AddNumber(int number) => number * 1;
        
    }
    public class Adder1For2 : INumberAdder
    {
        public int AddNumber(int number) => number * 2;

    }

}
