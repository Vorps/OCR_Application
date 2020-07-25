%%DistributionModel
function [Y] = DistributionModel(X)
    addpath('..\FeatureSelection');
    featuresRead = readtable('Features.dat');
    namesUtil = X.Properties.VariableNames;
    names = featuresRead.Properties.VariableNames;
    
    features = featuresRead(:, ismember(names, namesUtil));
    
    [group, id] = findgroups(features.Label);
    for it = 2:length(namesUtil)
        FeaturesS(:,it-1) = splitapply(@Features, featuresRead.(it), group) 
    end
    for it = 2:length(namesUtil)
        for it1 = 1:10
            tmp(it-1, it1, :) = FeaturesS(it1, it-1).percent(X.(it)); %Calcul de la proportion de la caracteristique au point X.(it)
        end
        total = sum(tmp(it-1, :, :));
        if total > 0
            res(it-1, :, :) = tmp(it-1, :, :)./total; %Renormalisation des proportions
        end
    end
    T = reshape(sum(res, 1),10,[],1); %Calcule de la classe la plus représenté
    [M,I] = max(T)
    Y = I-1;
end
