/* Copyright (c) 2013 Cloudbase.io Limited
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbase.DataCommands
{
    public enum CBDataAggregationGroupOperator
    {
        CBDataAggregationGroupSum = 0,
        CBDataAggregationGroupMax = 1,
        CBDataAggregationGroupMin = 2,
        CBDataAggregationGroupAvg = 3,
    }

    /**
     * The group aggregation command. This works exaclty in the same way a GROUP BY
     * command would work in SQL.
     * The outputField array contains the number of fields for the output to be
     * "grouped by".
     * There's a number of operators to apply to the grouped field defined as
     * CBDataAggregationGroupOperator
     */
    public class CBDataAggregationCommandGroup : CBDataAggregationCommand
    {
        private static string[] CBDataAggregationGroupOperator_ToString = { 
            "$sum",
            "$max",
            "$min",
            "$avg"
        };

        protected List<string> idFields;
        protected Dictionary<string, Dictionary<string, string>> groupFields;

        public CBDataAggregationCommandGroup()
        {
            this.CommandType = CBDataAggregationCommandType.CBDataAggregationGroup;
            this.idFields = new List<string>();
            this.groupFields = new Dictionary<string, Dictionary<string, string>>();
        }

        /**
	     * Adds a field to the list of fields the output should be
	     * grouped by
	     * @param An NSString with the name of the field
	     */
        public void AddOutputField(string fieldName)
        {
            this.idFields.Add("$" + fieldName);
        }

        /**
	     * Adds a calculated field to the output of this group clause using the value of another field
	     * @param outputFieldName The name of the output field
	     * @param operator The operator to apply to the selected variable field
	     * @param fieldName The name of the variable field to be used with the operator
	     */
        public void AddGroupFormulaForField(string outputFieldName, CBDataAggregationGroupOperator op, string fieldName)
        {
            this.AddGroupFormulaForValue(outputFieldName, op, "$" + fieldName);
        }

        /**
	     * Adds a calculated field to the output of this group clause using a static value
	     * @param outputFieldName The name of the output field
	     * @param op The operator to apply to the selected variable field
	     * @param value A value to be used with the operator
	     */
        public void AddGroupFormulaForValue(string outputFieldName, CBDataAggregationGroupOperator op, string value)
        {
            Dictionary<string, string> newOperator = new Dictionary<string, string>();
            newOperator.Add(CBDataAggregationGroupOperator_ToString[(int)op], value);
            this.groupFields.Add(outputFieldName, newOperator);
        }

        public override object SerializeAggregateConditions()
        {
            Dictionary<string, object> finalSet = new Dictionary<string, object>();

            if (this.idFields.Count > 1)
            {
                finalSet.Add("_id", this.idFields);
            }
            else
            {
                finalSet.Add("_id", this.idFields[0]);
            }

            foreach (var item in this.groupFields)
            {
                finalSet[item.Key] = item.Value;
            }

            return finalSet;
        }
    }
}
