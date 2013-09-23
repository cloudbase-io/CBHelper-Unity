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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloudbase.DataCommands
{
    /**
     * The project aggregation command filters the number of fields selected
     * from a document.
     * You can either populate the <strong>includeFields</strong> property
     * to exclude all fields and only include the ones selected or use
     * the <strong>excludeFields</strong> to set up an exclusion list.
     */
    public class CBDataAggregationCommandProject : CBDataAggregationCommand
    {
        public List<string> IncludeFields { get; set; }
        public List<string> ExcludeFields { get; set; }

        public CBDataAggregationCommandProject()
        {
            this.CommandType = CBDataAggregationCommandType.CBDataAggregationProject;
            this.IncludeFields = new List<string>();
            this.ExcludeFields = new List<string>();
        }

        public override object SerializeAggregateConditions()
        {
            Dictionary<string, int> fieldList = new Dictionary<string, int>();

            foreach ( string curField in this.IncludeFields )
            {
                fieldList.Add(curField, 1);
            }

            foreach (string curField in this.ExcludeFields)
            {
                fieldList.Add(curField, 0);
            }

            return fieldList;
        }
    }
}
