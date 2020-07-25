clc;clear all;close all;
%%
%Sélection des features sur critères statistique
%%
load featuresOption
featuresRead = readtable('Features.dat');
featuresName = featuresRead.Properties.VariableNames;
%% Génération des courbe ROC
ROC =  ROCCurve(featuresRead);
save('ROC.ma1t', 'ROC');
%% Reshape des données ROC
load ROC
for i = 2:width(featuresRead)
    for class1 = 1:10
        for class2 = 1:10
            if class1 ~=class2
                T(class1, class2, i-1) = ROC(class1,class2,i-1).AUC;
            end
         end
    end
end
%% Affiche bar3 les resultats AUC des courbes ROC
figure;
for i = 1:length(featuresName)-1
    subplot(floor(sqrt(length(featuresName)-1)),floor(sqrt(length(featuresName)-1))+1,i);
    bar3(T(:,:,i));
    title(featuresName(i+1));
end
%% Critère seuil
critere = T > 0.8;
idx = find(critere);
[classe1, classe2, feature]  = ind2sub([10,10, length(featuresName)-1], idx);
%% Critére N meilleur 
N = 3;
for i = 1:-0.001:0.5
    critere = T > i;
    idx = find(critere);
    [classe1, classe2, feature]  = ind2sub([10,10, length(featuresName)-1], idx);
    if length(unique(feature)) >= N
        break;
    end
end

%% Visualisation des features discriminiants fonction des classes
tableFeatureSelect = table(arrayfun(@(x) featuresName(x), feature), classe1, classe2)
heatmap(tableFeatureSelect, 'classe1', 'classe2');
title('Nombre de caractéristique garantissant un AUC > 0.8 entre classe');
%% Calcul de corrélation
clear corelation
xt = 1;
feat = unique(feature+1);
for x = feat'
    for y = feat'
        corelation{xt,1} = featuresName(x);
        corelation{xt,2} = featuresName(y);
        corelation{xt,3} = corr(featuresRead.(x), featuresRead.(y))
        xt = xt+1;
    end
end
corelationTable = cell2table(corelation, 'VariableNames', {'Feature1' 'Feature2' 'Value'});
%%
bar(corelationTable);
title("Mesure de corrélation entre caractéristique");
figure;
plot(featuresRead.(feat(2)), featuresRead.(feat(3)), '.');
featuresOptionSelectionIndex = [1; unique((feature+1))];
%%
save('FeaturesOptionSelectionIndex.mat', 'featuresOptionSelectionIndex');