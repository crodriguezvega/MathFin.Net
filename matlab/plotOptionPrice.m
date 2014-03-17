% Read matrix with option prices from text file
M = dlmread('OptionPrice.txt');

% Plot option price surface
[S, t] = meshgrid(linspace(0, 200, 51), linspace(0, 1, 101));
hqr = surf(S, t,  M);

xlabel('Spot price');
ylabel('Time');
zlabel('Option price');
set(hqr, 'FaceAlpha', .6);
set(hqr, 'EdgeAlpha', .2);
set(hqr, 'FaceLighting', 'phong’);
set(hqr, 'FaceColor', 'interp’);
set(hqr, 'FaceAlpha', .6);