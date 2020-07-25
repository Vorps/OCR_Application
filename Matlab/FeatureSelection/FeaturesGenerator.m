%%
% Génération des caracteristiques de 20000 images
%%
clc;clear all;
addpath('..\MNIST');
size = 20000;
% Choix des features
featuresOption = {'Label' ,{'Intersection_X', @(st, image) Intersection(image, 'X')}, {'Intersection_Y', @(st, image) Intersection(image, 'Y')},  {'Density', @(st, image) sum(sum(image > 0, 1))}, 'Area', {'AxisRatio', @(st, image) st.MinorAxisLength/st.MajorAxisLength},'Eccentricity','Orientation','ConvexArea','Circularity','FilledArea','EulerNumber','EquivDiameter','Solidity','Extent','Perimeter','PerimeterOld', {'LW', @(st, image) st.BoundingBox(4)/st.BoundingBox(3)}};
% Load 2000 images from MNIST
[images, labels] = readMNIST('train-images.idx3-ubyte', 'train-labels.idx1-ubyte', size, 0);
writetable(Descriptor(images, labels, featuresOption), 'Features.dat');
save('FeaturesOption.mat', 'featuresOption')