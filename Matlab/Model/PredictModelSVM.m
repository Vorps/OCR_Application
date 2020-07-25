%Prédiction sur le model SVM
function [Y] = PredictModelSVM(X)
    load('trainedModel');
    Y = cell2mat(predict(trainedModel.ClassificationSVM, extractHOGFeatures(X)));
end