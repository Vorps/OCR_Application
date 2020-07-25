%% Init
clc;clear all; close all;
%% Data for train
addpath('..\MNIST')
[images, labels] = readMNIST('train-images.idx3-ubyte', 'train-labels.idx1-ubyte', 20000, 0);
images = images > 200;
featuresOption = {'Label' , {'HOG', @(st, image) extractHOGFeatures(image)}};
feature = Descriptor(images, labels, featuresOption);
%% Save model
save(['..\Models\' 'trainedModel'])