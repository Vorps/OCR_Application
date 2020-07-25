%%
% Fonction d'extraction de caractéristique
function [featuresTable] = Descriptor(images, labels, featuresOption)
it = 1;
for i = 1:size(images, 3)
    st = regionprops(images(:,:,i) > 0,images(:,:,i), 'All' );
    if length(st) == 1
        for ot = 1:length(featuresOption)
            if ischar(featuresOption{ot}) % Description regionprops
                if it == 1
                    name{ot} = featuresOption{ot};
                end
                if isfield(st, featuresOption{ot})
                    features{it, ot} = getfield(st, featuresOption{ot});
                end
            else % Description regionprops function transform
                if it == 1
                     name{ot} = featuresOption{ot}{1};
                end
                features{it, ot} =featuresOption{ot}{2}(st, images(:,:,it) > 0);
            end
        end
        if ~isempty(labels) %Ajout des labels
            features{it,1} =  num2str(labels(it));
        end
        it = it +1;
    end
end
featuresTable = cell2table(features, 'VariableNames', name);
end
