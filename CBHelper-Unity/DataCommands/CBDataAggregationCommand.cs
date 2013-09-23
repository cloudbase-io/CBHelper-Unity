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
    public enum CBDataAggregationCommandType
    {
        CBDataAggregationProject = 0,
        CBDataAggregationUnwind = 1,
        CBDataAggregationGroup = 2,
        CBDataAggregationMatch = 3,
    }

    /**
     * This abstract class should be implemented by any class to send 
     * Data Aggregation commands to cloudbase.io
     *
     * The serializeAggregateConditions should resturn a Map
     * exactly in the format needed by the CBHelper class to be added
     * to the list of parmeters, serliazed and sent to cloudbase.io
     */
    public abstract class CBDataAggregationCommand
    {
        private static string[] CBDataAggregationCommandType_ToString = { 
            "$project",
            "$unwind",
            "$group",
            "$match"
        };

        public CBDataAggregationCommandType CommandType { get; set; }

        public string GetCommandTypeString() {
            return CBDataAggregationCommandType_ToString[(int)this.CommandType];
        }

        /**
	     * Serializes the Command object to its JSON representation
	     *
	     * @return An Object representation of the Command object. This
	     *  method should be implemented in each subclass
	     */
        public abstract object SerializeAggregateConditions();
    }
}
