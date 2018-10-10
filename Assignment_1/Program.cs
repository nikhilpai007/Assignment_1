/* COIS 2020 Data Structures and Algorithms Assignment 1 - Source Code 
 * Done by: 
 *          Nikhil Pai Ganesh - 0595517 
 *          Mari Phkhakadze - 0608057
 *          */





// Task 1: Term Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polynomialCalculation
{
    public class Term : IComparable
    {
        private double Coefficient;///private member for coefficient purpose
        private byte Exponent;///private member for exponent purpose
        // Creates a term with the given coefficient and exponent  
        public Term(double coefficient, byte exponent)////constructor
        {
            /////setting private members///////////////
            this.Coefficient = coefficient;
            this.Exponent = exponent;

        }
        public void setCoefficient(double c)
        {
            //////////setting coefficient
            Coefficient = c;
        }
        // Evaluates the current term for a given x   
        public double Evaluate(double x)
        {
            ////Evaluate functtion which calculates a term with a given value of x
            double cal = Math.Pow(x, Exponent);
            return Coefficient * cal;//returning the result
        }
        // Returns -1, 0, or 1 if the exponent of the current term  // is less than, equal to, or greater than the exponent of obj.  
        public int CompareTo(object obj)
        {
            Term t = (Term)obj;///type casting to TERM 
            if (this.Exponent < (t.Exponent))
            {
                return -1;
            }
            else if (this.Exponent == t.Exponent)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }
        public byte getExponent()
        {
            return this.Exponent;//returning the exponent
        }

        public double getCoefficient()
        {
            return this.Coefficient;//returning the coefficient
        }


    }
}


// Task 2: Node Class

namespace polynomialCalculation
{
    ///Node class/////
    public class Node<T>
    {
        private T item { get; set; }
        private Node<T> next { get; set; }
        public Node(T ite, Node<T> net)///constructor
        {
            ///setting private members
            this.item = ite;
            this.next = net;
        }
        public Node(T ite)///parametric constructor
        {
            ///setting private members
            this.item = ite;
            this.next = null;
        }
        public Node()///default constructor
        {
            this.next = null;
        }
        public T getItem()
        {
            return item;///returning the item
        }
        public Node<T> getNext()
        {
            return next;
        }
        public void setNext(Node<T> net)
        {
            this.next = net;
        }

    }
}


// Task-3 :  Polynomial Class

interface IDegree
{
    bool Order(Object obj);
}

namespace polynomialCalculation
{
    ///Polynomial class
    public class Polynomial : IDegree
    {
        private Node<Term> front;

        // Creates the polynomial 0  
        public Polynomial()///default constructor
        {
            front = null;
        }
        // Inserts the given term t into the current polynomial in its proper order  
        ///Add term function which adds the term in polynomial in order
        public Polynomial AddTerm(Term t)
        {
            if (front == null)///checking the start
            {
                front = new Node<Term>(t);///creating a node
                return this;
            }
            /////variables used for tracing through nodes
            Node<Term> temp = front;
            Node<Term> pre = front;
            Node<Term> newNode = new Node<Term>(t);////creating new node which has to add in the polynomial
            if (temp.getNext() == null)///checking the next node
            {
                if (temp.getItem().getExponent() > t.getExponent())///checking the exponents
                {
                    temp.setNext(newNode);///setting the next node
                }
                else if (temp.getItem().getExponent() == t.getExponent())//if exponents are equal
                {
                    temp.getItem().setCoefficient(temp.getItem().getCoefficient() + t.getCoefficient());///adding the terms
                }
                else
                {
                    newNode.setNext(temp);///adding new node in the start of polnomial
                    front = newNode;//setting the front
                }
                return this;
            }
            while (temp.getNext() != null)///checking the next node
            {
                while (temp.getItem().getExponent() > t.getExponent())///loop until power condition turns false
                {
                    pre = temp;
                    if (temp.getNext() != null)///checking next node
                    {
                        temp = temp.getNext();//moving to next node

                    }
                    else
                    {
                        temp.setNext(newNode);//setting next node
                        return this;
                    }
                }
                if (temp.getItem().getExponent() == t.getExponent())///checking the exponents equal or not
                {
                    temp.getItem().setCoefficient(temp.getItem().getCoefficient() + t.getCoefficient());//if equal then add the coefficients
                    return this;
                }
                if (pre == front && pre.getItem().getExponent() == t.getExponent())///if only 1 node exist
                {
                    pre.getItem().setCoefficient(pre.getItem().getCoefficient() + t.getCoefficient());
                    return this;
                }
                if (pre == front && pre.getItem().getExponent() == t.getExponent())
                {
                    pre.getItem().setCoefficient(pre.getItem().getCoefficient() + t.getCoefficient());
                    return this;
                }
                if (pre == front && pre.getItem().getExponent() > t.getExponent())/// new nodes exponent is greater then 1st node but less then 2nd node
                {
                    newNode.setNext(pre.getNext());///adding new node between 2 nodes
                    pre.setNext(newNode);//setting the next node
                    return this;
                }
                if (pre == front)///only 1 node exist whose exponent is less then new node
                {
                    newNode.setNext(front);//setting the next node
                    front = newNode;///setting the front
                    return this;
                }
                if (pre.getItem().getExponent() == t.getExponent())
                {
                    pre.getItem().setCoefficient(pre.getItem().getCoefficient() + t.getCoefficient());
                    return this;
                }

                pre.setNext(newNode);///setting the pointer of previous node
                newNode.setNext(temp);//setting the pointer of new node
                return this;

            }

            return this;
        }
        public bool Order(Object obj)
        {
            Polynomial p = (Polynomial)obj;
            if (this.front.getItem().getExponent() >= p.front.getItem().getExponent())///checking the exponent
            {
                return true;
            }
            else
                return false;
        }
        public static Polynomial operator +(Polynomial p, Polynomial q)
        {
            Polynomial result = new Polynomial();
            Node<Term> pfront = p.front;
            Node<Term> qfront = q.front;
            while (pfront != null || qfront != null)///loop until end of 2 polynomials
            {

                if (qfront != null)
                {
                    Term t = new Term(qfront.getItem().getCoefficient(), qfront.getItem().getExponent());//creating a new term
                    result.AddTerm(t);//adding the term in the result
                    qfront = qfront.getNext();///moving to next node

                }
                if (pfront != null)
                {
                    Term t = new Term(pfront.getItem().getCoefficient(), pfront.getItem().getExponent());//creating a new term
                    result.AddTerm(t);//adding the term in the result
                    pfront = pfront.getNext();///moving to next node
                }

            }
            return result;///returning the result

        }

        public static Polynomial operator *(Polynomial p, Polynomial q)
        {

            Polynomial result = new Polynomial();
            Node<Term> pfront = p.front;
            Node<Term> qfront = q.front;
            int psize = 0, qsize = 0;
            while (pfront != null)
            {
                psize++;///finding the size of 1st polynomial
                pfront = pfront.getNext();
            }
            while (qfront != null)
            {
                qsize++;///finding the size of 2nd polynomial
                qfront = qfront.getNext();
            }
            if (psize == 0)
            {
                result = q;///if 1st polynomial is null then setting the result to 2nd polynomial
            }
            if (qsize == 0)
            {
                result = p;///if 2nd polynomial is null then setting the result to 1st polynomial
            }
            for (pfront = p.front; pfront != null; pfront = pfront.getNext())///loop until the end of 1st polynomial
            {
                for (qfront = q.front; qfront != null; qfront = qfront.getNext())///loop until the end of 2nd polynomial
                {
                    double c = pfront.getItem().getCoefficient() * qfront.getItem().getCoefficient();///multiplying the coeficients
                    byte e = pfront.getItem().getExponent();
                    byte ee = qfront.getItem().getExponent();
                    int h = e + ee;///adding the exponents
                    Term t = new Term(c, (byte)h);//creating a new term
                    result.AddTerm(t);//adding the term in the result

                }
            }
            return result;//returning the result
        }

        public double Evaluate(double x)
        {
            Node<Term> temp = this.front;
            double result = 0;
            while (temp != null)
            {
                double power = Math.Pow(x, temp.getItem().getExponent());///finding the exponent using math function
                result = result + (temp.getItem().getCoefficient() * power);///multiplying the coeficient with the power result
                temp = temp.getNext();///moving to next node
            }
            return result;///returning the result
        }
        public void Print()
        {
            Node<Term> temp = front;
            while (temp != null)
            {
                Console.Write(temp.getItem().getCoefficient() + "x ^ " + temp.getItem().getExponent());///displaying the polynomial
                if (temp.getNext() != null)
                    Console.Write(" + ");
                temp = temp.getNext();
            }
        }

    }
}



// Task 4: Polynomials Class



namespace polynomialCalculation
{
    public class Polynomials
    {
        private List<Polynomial> P;///list of polynomials
        public Polynomials()///default constructor
        {
            P = new List<Polynomial>();
        }
        public Polynomial Retrieve(int k)////a function which returns a polynomial from the list 
        {
            return P.ElementAt(k - 1);///returning the polynomial at given index

        }
        public void Insert(Polynomial p)
        {
            //P.Add(p);
            int size = P.Count;///counting the size of list
            for (int i = 0; i < size; i++)
            {
                Polynomial a = new Polynomial();
                a = P[i];
                if (a.Order(p) == false)///checking the order of polynomial
                {
                    P.Insert(i, p);///inserting the polynomial in the list
                    return;
                }
            }
            P.Add(p);///adding the polynomial in the list

        }
        public void Delete(int i)
        {
            P.RemoveAt(i - 1);///removing a polynomial at given index
        }
        public void Print()
        {

            /////displaying the list of polnomials////////////// 
            if (P.Count == 0)
                Console.WriteLine("         List is Empty");
            if (P.Count != 0)
                Console.WriteLine("         List has the following Polnomials");
            for (int i = 0; i < P.Count; i++)
            {
                Console.Write("index:" + (i + 1) + " ) ");
                P[i].Print();
                Console.WriteLine("");
            }
        }

    }
}




// Task 5: Main Program


namespace polynomialCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            Polynomials listP = new Polynomials();
            while (option != 6)
            {

                Console.WriteLine("********************************************");
                listP.Print();
                Console.WriteLine("********************************************");
                Console.WriteLine("1- Create a polynomial and insert it into List");
                Console.WriteLine("2- Add two polynomials (retrieved by index) and  insert the resultant polynomial into the list");
                Console.WriteLine("3- Multiply two polynomials (retrieved by index) and  insert the resultant polynomial into the list");
                Console.WriteLine("4- Delete a polynomial (at a given index)");
                Console.WriteLine("5- To evaluate a given polynomial(retrieved by index)");
                Console.WriteLine("6- for Exit");
                Console.WriteLine(" Enter your option: ");
                option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
                {
                    int terms = 0;
                    Console.WriteLine(" Enter number of terms you want in the polnomial: ");
                    terms = Convert.ToInt32(Console.ReadLine());//taking integer input
                    Polynomial p = new Polynomial();//creating a polynomial
                    for (int i = 0; i < terms; i++)
                    {
                        double cf = 0;
                        int pw = 0;
                        //taking input of coefficicent and power
                        Console.WriteLine(" Enter  the Coefficient for term" + (i + 1));
                        cf = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(" Enter  the Exponent for term" + (i + 1));
                        pw = Convert.ToInt32(Console.ReadLine());
                        p.AddTerm(new Term(cf, (byte)pw));///adding a new term in the polnomial
                    }
                    listP.Insert(p);///adding the polnomial in the list
                }
                if (option == 2)
                {
                    int one = 0, two = 0;
                    Console.WriteLine(" Enter  the index for Polynomial 1");
                    one = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(" Enter  the index for Polynomial 2");
                    two = Convert.ToInt32(Console.ReadLine());
                    listP.Insert(listP.Retrieve(one) + listP.Retrieve(two));///adding 2 polynomials and storing the result in the list
                }
                if (option == 3)
                {
                    int one = 0, two = 0;
                    Console.WriteLine(" Enter  the index for Polynomial 1");
                    one = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(" Enter  the index for Polynomial 2");
                    two = Convert.ToInt32(Console.ReadLine());
                    listP.Insert(listP.Retrieve(one) * listP.Retrieve(two));//adding 2 polynomials and storing the result in the list
                }
                if (option == 4)
                {
                    int one = 0;
                    Console.WriteLine(" Enter  the index for Polynomial which you want to delete");
                    one = Convert.ToInt32(Console.ReadLine());
                    listP.Delete(one);///deleting a polynomial at given index
                }
                if (option == 5)
                {
                    int one = 0, x = 0;
                    Console.WriteLine(" Enter  the index for Polynomial which you want to evaluate");
                    one = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(" Enter  the value of variable (x) in the polynomial");
                    x = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Answer is " + listP.Retrieve(one).Evaluate(x));//evaluating a polynomial with given value of x
                    Console.WriteLine("--------------------------------");
                }
            }



        }
    }
}
