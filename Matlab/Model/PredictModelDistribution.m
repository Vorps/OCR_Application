%% Fonction de prediction du model distribution
function [Y] = PredictModelDistribution(X)
    addpath('..\FeatureSelection');
    load FeaturesOption
    load FeaturesOptionSelectionIndex
    featuresTest = Descriptor(X, [], featuresOption(featuresOptionSelectionIndex));
    Y = num2str(DistributionModel(featuresTest));
end