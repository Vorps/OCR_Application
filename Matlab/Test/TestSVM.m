% Code de test du model SVM
clear all;clc;close all;
%% Init
addpath('..\Model')
addpath('..\MNIST')
%% Load images test
[images, labels] = readMNIST('train-images.idx3-ubyte', 'train-labels.idx1-ubyte', 4000, 20000);
images = images > 200;
load trainedModel

%% Prédiction
for i = 1:4000
    labelPredic(i) = predict(trainedModel.ClassificationSVM, extractHOGFeatures(images(:,:,i)));
end
%%
%% Résultat
labelNumPredict = str2double(labelPredic)';
Score = sum(labelNumPredict == labels)/length(labelNumPredict);
C = confusionmat(labels,labelNumPredict)
confusionchart(C);
title('Matrice de confusion');
disp(['Score : ', Score]);