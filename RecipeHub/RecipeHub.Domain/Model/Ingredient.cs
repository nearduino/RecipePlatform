using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model.Enums;
using RecipeHub.Domain.Model.Exceptions;
using RecipeHub.Domain.Utilities;

namespace RecipeHub.Domain.Model
{
    public class Ingredient
    {
        public Guid Id { get; private set; }
        public int CaloriesPerUnit { get; private set; }
        public string Name { get; private set; }
        public MeasureUnit MeasureUnit { get; private set; }

        private Ingredient(){}

        public Ingredient(int calories, string name, MeasureUnit measureUnit, Guid id)
        {
            Id = id;
            CaloriesPerUnit = calories;
            Name = name;
            MeasureUnit = measureUnit;
            Validate();
        }

        public Ingredient(int calories, string name, MeasureUnit measureUnit)
        {
            CaloriesPerUnit = calories;
            Name = name;
            MeasureUnit = measureUnit;
            Validate();
        }

        private void Validate()
        {
            if (CaloriesPerUnit <= 0) throw new InvalidCaloriesException();
            if (!TextValidator.CheckName(Name)) throw new InvalidNameException();
        }

    }
}
