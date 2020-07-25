%%Calcul des AUC conbinaison feature,classes
function output = ROCCurve(input)
    for f = 2:width(input)
        for class1Id = 0:9
            for class2Id = 0:9
                if class1Id ~= class2Id
                    class1 = input.Label == class1Id;
                    class2 = input.Label == class2Id;
                    test = class1 | class2;
                    X = cat(2, input.(f)(test), arrayfun(@(x) x == class2Id, input.Label(test)));
                    ROCout = roc(X, 'verbose', 0);
                    output(class1Id+1, class2Id+1, f-1).AUC= ROCout.AUC;
                end
            end
        end
    end
end