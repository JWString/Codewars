//https://www.codewars.com/kata/526c7b931666d07889000a3c

using System;
using System.Collections.Generic;
using System.IO;

namespace Solutions.BefungeInterpreter
{
  public class BefungeInterpreter {
      
    abstract class Interpreter
    {
      protected enum HorizontalDirection {
        Left = -1,
        Right = 1,
        None = 0
      }
      protected enum VerticalDirection {
        Up = -1,
        Down = 1,
        None = 0
      }
      protected string[]? _input;
      protected StringWriter _output = new StringWriter();
      protected Stack<int> _stack = new Stack<int>();
      protected int _x = 0;
      protected int _y = 0;
      protected HorizontalDirection _hDelta = HorizontalDirection.Right;
      protected VerticalDirection _vDelta = VerticalDirection.None;
      protected bool _complete = false;
      protected Interpreter() { }
      protected Interpreter(string[] input) {
        _input = input;
      }
      public bool Advance() {
        if (!_complete) {
          _x += (int)_hDelta;
          _y += (int)_vDelta;        
        }
        if (_y < 0 || _y >= _input.Length || _x < 0 || _x >= _input[_y].Length) {
          throw new Exception($"Interpreter moved out of bounds: (x: {_x}, y: {_y})");
        }
        return _complete;
      }
      public Interpreter ToDefaultInterpreter() {
        return new DefaultInterpreter(null)
        {
          _input = _input,
          _output = _output,
          _stack = _stack,
          _x = _x,
          _y = _y,
          _hDelta = _hDelta,
          _vDelta = _vDelta,
          _complete = _complete
        };
      }
      public Interpreter ToStringInterpreter() {
        return new StringInterpreter
        {
          _input = _input,
          _output = _output,
          _stack = _stack,
          _x = _x,
          _y = _y,
          _hDelta = _hDelta,
          _vDelta = _vDelta,
          _complete = _complete
        };
      }
      public string Output { get => _output.ToString(); }
      abstract public Interpreter Interpret();
    }
    
    class DefaultInterpreter : Interpreter
    {
      public DefaultInterpreter(string[] input) : base(input) { }
      override public Interpreter Interpret() {
        var c = _input[_y][_x];
        switch (c) {
          case '+': {
            var a = _stack.Pop();
            var b = _stack.Pop();
            _stack.Push(a + b);
            return this;            
          }
          case '-': {
            var a = _stack.Pop();
            var b = _stack.Pop();
            _stack.Push(b - a);
            return this;            
          }
          case '*': {
            var a = _stack.Pop();
            var b = _stack.Pop();
            _stack.Push(a * b);
            return this;            
          }
          case '/': {
            var a = _stack.Pop();
            var b = _stack.Pop();
            _stack.Push(a == 0 ? 0 : b / a);
            return this;            
          }
          case '%': {
            var a = _stack.Pop();
            var b = _stack.Pop();
            _stack.Push(a == 0 ? 0 : b % a);
            return this;            
          }
          case '!': {
            var a = _stack.Pop();
            _stack.Push(a == 0 ? 1 : 0);
            return this;            
          }
          case '`': {
            var a = _stack.Pop();
            var b = _stack.Pop();
            _stack.Push(b > a ? 1 : 0);
            return this;            
          }
          case '>': {
            _hDelta = HorizontalDirection.Right;
            _vDelta = VerticalDirection.None;
            return this;            
          }
          case '<': {
            _hDelta = HorizontalDirection.Left;
            _vDelta = VerticalDirection.None;
            return this;            
          }
          case '^': {
            _hDelta = HorizontalDirection.None;
            _vDelta = VerticalDirection.Up;
            return this;            
          }
          case 'v': {
            _hDelta = HorizontalDirection.None;
            _vDelta = VerticalDirection.Down;
            return this;            
          }
          case '?': {
            var d = new Random().Next(4);
            _hDelta = d == 0
              ? HorizontalDirection.Left
              : d == 1
                ? HorizontalDirection.Right
                : HorizontalDirection.None;
            _vDelta = d == 2
              ? VerticalDirection.Up
              : d == 3
                ? VerticalDirection.Down
                : VerticalDirection.None;
            return this;
          }
          case '_': {
            var a = _stack.Pop();
            _hDelta = a == 0
              ? HorizontalDirection.Right
              : HorizontalDirection.Left;
            _vDelta = VerticalDirection.None;       
            return this;
          }
          case '|': {
            var a = _stack.Pop();
            _hDelta = HorizontalDirection.None;
            _vDelta = a == 0
              ? VerticalDirection.Down
              : VerticalDirection.Up;
            return this;
          }
          case '"': {
            return this.ToStringInterpreter();
          }
          case ':': {
            _stack.Push(_stack.Count == 0 ? 0 : _stack.Peek());
            return this;
          }
          case '\\': {
            var a = _stack.Pop();
            var b = _stack.Count == 0 ? 0 : _stack.Pop();
            _stack.Push(a);
            _stack.Push(b);
            return this;
          }
          case '$': {
            _stack.Pop();
            return this;
          }
          case '.': {
            _output.Write(_stack.Pop());
            return this;
          }
          case ',': {
            _output.Write((char)_stack.Pop());
            return this;
          }
          case '#': {
            Advance();
            return this;
          }
          case 'p': {
            var y = _stack.Pop();
            var x = _stack.Pop();
            var v = _stack.Pop();
            _input[y] = _input[y].Substring(0, x) + (char)v + _input[y].Substring(x + 1);
            return this;
          }
          case 'g': {
            var y = _stack.Pop();
            var x = _stack.Pop();
            _stack.Push((int)_input[y][x]);
            return this;
          }
          case '@': {
            _complete = true;
            return this;
          }
          case ' ': {
            return this;
          }
          default: {
            if (int.TryParse(c.ToString(), out int i)) {
              _stack.Push(i);
              return this;
            }
            throw new Exception("Unhandled symbol: " + c);
          }
        }
      }
    }

    class StringInterpreter : Interpreter {
      override public Interpreter Interpret() {
        var c = _input[_y][_x];
        switch (c) {
          case '"': {
            return this.ToDefaultInterpreter();
          }
          default: {
            _stack.Push((int)c);
            return this;
          }
        }
      }
    }  

    public string Interpret(string code) {
      Console.Write(code);
      var input = code.Split("\n");
      Interpreter interpreter = new DefaultInterpreter(input);
      do {
        interpreter = interpreter.Interpret();      
      } while (!interpreter.Advance());
      return interpreter.Output;
    }
  }
}

