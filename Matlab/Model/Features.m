%Objets pour les features model distribution
classdef Features
   properties
       valuesReal;
       values;
       min;
       binWidth;
   end
   methods
      function obj = Features(values)
            obj.valuesReal = values;
           obj.values = histcounts(values, 100);
           obj.binWidth = (max(values)-min(values))/100; %Calcul du binWidth pour échantilionner la répartition parmit sa classe
           obj.min = min(values);
      end
      function result = percent(obj, values)
          result = zeros(length(values),1);
          index = floor((values-obj.min)/obj.binWidth)+1;
          it = 1;
          for i = index'
            if( i > 0 &&  i <= length(obj.values))
                result(it) = obj.values(i)*obj.binWidth; 
            end
            it = it + 1;
          end
      end
      function [back, next] = roc(obj, index)
              back = sum(obj.values(1:index))*obj.binWidth;
              next =  sum(obj.values(index:end))*obj.binWidth;
      end
   end
end