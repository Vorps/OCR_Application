% Code de test du model distribution
clc;clear all;close all;
addpath('..\FeatureSelection');
addpath('..\Model');
addpath('..\MNIST');
load FeaturesOption
load FeaturesOptionSelectionIndex
%% Load images test
[imageTest, labelsTest] = readMNIST('train-images.idx3-ubyte', 'train-labels.idx1-ubyte', 4000, 20000);
%% Load features
featuresTest = Descriptor(imageTest, labelsTest, featuresOption(featuresOptionSelectionIndex));
Y = DistributionModel(featuresTest); %Prediction
%% Résultat
labelRealNum = str2num(cell2mat(featuresTest.Label))'
C = confusionmat(Y,labelRealNum)
figure;
sum(Y == labelRealNum)/length(labelRealNum)
confusionchart(C);
title('Matrice de confusion')
