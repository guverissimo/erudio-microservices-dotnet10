using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestWithASPNETUdemy.Controllers
{
    [Route("[controller]")]
    public class CalculatorController : Controller
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sqrt/{a}")]
        public IActionResult Sqrt(string a)
        {
            if (isNumeric(a))
            {
                var number = ConvertToDecimal(a);
                if (number < 0)
                {
                    return BadRequest("Cannot calculate square root of a negative number");
                }
                var result = Math.Sqrt((double)number);
                return Ok(result.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("media/{a}/{b}")]
        public IActionResult Media(string a, string b)
        {
            if(isNumeric(a) && isNumeric(b))
            {
                var result = (ConvertToDecimal(a) + ConvertToDecimal(b)) / 2;
                return Ok(result.ToString());
            }
            return BadRequest("Invalid input");
        }


        [HttpGet("mult/{a}/{b}")]
        public IActionResult Mult(string a, string b)
        {
            if (isNumeric(a) && isNumeric(b))
            {
                var result = ConvertToDecimal(a) * ConvertToDecimal(b);
                return Ok(result.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("div/{a}/{b}")]
        public IActionResult Div(string a, string b)
        {
            if (isNumeric(a) && isNumeric(b))
            {
                if (ConvertToDecimal(b) == 0)
                {
                    return BadRequest("Division by zero is not allowed");
                }
                var result = ConvertToDecimal(a) / ConvertToDecimal(b);
                return Ok(result.ToString());

            }
            return BadRequest("Invalid input");
        }

        [HttpGet("sub/{a}/{b}")]
        public IActionResult Sub(string a, string b)
        {
            if (isNumeric(a) && isNumeric(b))
            {
                var sum = ConvertToDecimal(a) - ConvertToDecimal(b);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("sum/{a}/{b}")]
        public IActionResult Add(string a, string b)
        {
            if (isNumeric(a) && isNumeric(b))
            {
                var sum = ConvertToDecimal(a) + ConvertToDecimal(b);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid input");
        }

        private bool isNumeric(string strNum)
        {
            decimal num;
            bool isNumber = decimal.TryParse(
                strNum, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out num);
            return isNumber;
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            if (decimal.TryParse(strNumber, out var decimalValue))
            {
                return decimalValue;
            }
            throw new ArgumentException("Invalid number format");

        }
    }
}