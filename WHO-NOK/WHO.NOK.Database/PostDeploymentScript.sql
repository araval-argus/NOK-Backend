-- -- This file contains SQL statements that will be executed after the build script.

-- -- Inserts Languages if the table is empty
-- IF NOT EXISTS(SELECT *
-- FROM [dbo].[Languages])
-- BEGIN
--     INSERT INTO [dbo].[Languages]
--     VALUES
--         (1, 'Arabic', 'ar'),
--         (2, 'Chinese', 'zh'),
--         (3, 'English', 'eng'),
--         (4, 'French', 'fr'),
--         (5, 'Russian', 'ru'),
--         (6, 'Spanish', 'es')
-- END

-- --Inserts Roles if the table is empty
IF NOT EXISTS (SELECT *
               FROM [dbo].[Roles])
BEGIN
    SET IDENTITY_INSERT [dbo].[Roles] ON; -- Enable explicit identity insert

    INSERT INTO [dbo].[Roles] ([RoleId], [Name], [Description])
    VALUES
        (1, 'System Admin', 'System Administrator'),
        (2, 'Reviewer', 'Reviewer'),
        (3, 'Contributor', 'Contributor'),
        (4, 'Viewer', 'Viewer');

    SET IDENTITY_INSERT [dbo].[Roles] OFF; -- Disable explicit identity insert
END


-- IF( NOT EXISTS (SELECT TOP 1
--     1
-- FROM DetailedActivityTypes))
-- BEGIN
--     INSERT INTO DetailedActivityTypes
--         (Activity, CreatedAt, CreatedBy, LastUpdatedAt, LastUpdatedBy)
--     VALUES
--         ('Advocacy', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Assessment and data use', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Coordination/collaboration', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Designation', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Dissemination', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Financing', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('HR', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Infrastructure', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Meeting/workshop', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Monitoring and Evaluation', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Planning and strategy', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Procurement', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Programme implementation', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('SOPs/Policy', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Tool development', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Training', GETUTCDATE(), 1, GETUTCDATE(), 1),
--         ('Other', GETUTCDATE(), 1, GETUTCDATE(), 1)
-- END

IF NOT EXISTS(SELECT *
FROM [dbo].[Countries])
BEGIN
INSERT INTO [dbo].[Countries]
    ([Name], [ISOCode], [ISO3Code], [Latitude], [Longitude], [Region])
VALUES
    (N'India', N'IN', N'IND', CAST(22.881692696914500 AS Decimal(20, 15)), CAST(79.621862064792200 AS Decimal(20, 15)), N'SEARO'),
    (N'USA', N'US', N'USA', CAST(37.090240 AS Decimal(20, 15)), -CAST(95.712891 AS Decimal(20, 15)), N'AMRO'),
    (N'Netherlands (Kingdom of the)', N'NL', N'NLD', CAST(52.253620618450700 AS Decimal(20, 15)), CAST(5.601053071947280 AS Decimal(20, 15)), N'EURO'),
    (N'Afghanistan', N'AF', N'AFG', CAST(33.838947593081700 AS Decimal(20, 15)), CAST(66.026591212120300 AS Decimal(20, 15)), N'EMRO'),
    (N'United Kingdom of Great Britain and Northern Ireland', N'GB', N'GBR', CAST(51.507400 AS Decimal(20,15)), CAST(-0.127800 AS Decimal(20,15)), N'EURO'),
    (N'United Arab Emirates', N'AE', N'ARE', CAST(23.912672414678400 AS Decimal(20, 15)), CAST(54.332188377662900 AS Decimal(20, 15)), N'EMRO'),
    (N'Argentina', N'AR', N'ARG', CAST(-35.376645076916700 AS Decimal(20, 15)), CAST(-65.167453269334300 AS Decimal(20, 15)), N'AMRO'),
    (N'United States of America', N'US', N'USA', CAST(45.693716538593900 AS Decimal(20, 15)), CAST(-112.571453802407000 AS Decimal(20, 15)), N'AMRO'),
    (N'Antigua and Barbuda', N'AG', N'ATG', CAST(17.279670730610900 AS Decimal(20, 15)), CAST(-61.791939063759600 AS Decimal(20, 15)), N'AMRO'),
    (N'Burundi', N'BI', N'BDI', CAST(-3.356269594208510 AS Decimal(20, 15)), CAST(29.886856557189800 AS Decimal(20, 15)), N'AFRO'),
    (N'Benin', N'BJ', N'BEN', CAST(9.647688495128930 AS Decimal(20, 15)), CAST(2.343317800285080 AS Decimal(20, 15)), N'AFRO'),
    (N'Burkina Faso', N'BF', N'BFA', CAST(12.277959381766700 AS Decimal(20, 15)), CAST(-1.739782796019670 AS Decimal(20, 15)), N'AFRO'),
    (N'Bangladesh', N'BD', N'BGD', CAST(23.832934697486400 AS Decimal(20, 15)), CAST(90.271339754299600 AS Decimal(20, 15)), N'SEARO'),
    (N'Bahrain', N'BH', N'BHR', CAST(26.019460426792000 AS Decimal(20, 15)), CAST(50.562542348647400 AS Decimal(20, 15)), N'EMRO'),
    (N'Bahamas', N'BS', N'BHS', CAST(24.171417620100400 AS Decimal(20, 15)), CAST(-76.508878703420000 AS Decimal(20, 15)), N'AMRO'),
    (N'France', N'FR', N'FRA', CAST(46.564920108242900 AS Decimal(20, 15)), CAST(2.550469262195100 AS Decimal(20, 15)), N'EURO'),
    (N'Belize', N'BZ', N'BLZ', CAST(17.218543486459600 AS Decimal(20, 15)), CAST(-88.682803063929400 AS Decimal(20, 15)), N'AMRO'),
    (N'Plurinational State of Bolivia', N'BO', N'BOL', -CAST(16.290131 AS Decimal(20, 15)), -CAST(63.588917 AS Decimal(20, 15)), N'AMRO'),
    (N'Brazil', N'BR', N'BRA', CAST(-10.772242660211300 AS Decimal(20, 15)), CAST(-53.088724197932100 AS Decimal(20, 15)), N'AMRO'),
    (N'Barbados', N'BB', N'BRB', CAST(13.178858857450700 AS Decimal(20, 15)), CAST(-59.562104834700600 AS Decimal(20, 15)), N'AMRO'),
    (N'Brunei Darussalam', N'BN', N'BRN', CAST(4.521564945123890 AS Decimal(20, 15)), CAST(114.761378593636000 AS Decimal(20, 15)), N'WPRO'),
    (N'Bhutan', N'BT', N'BTN', CAST(27.415563923764300 AS Decimal(20, 15)), CAST(90.429491776014700 AS Decimal(20, 15)), N'SEARO'),
    (N'Botswana', N'BW', N'BWA', CAST(-22.182004499010600 AS Decimal(20, 15)), CAST(23.815120564571700 AS Decimal(20, 15)), N'AFRO'),
    (N'Central African Republic', N'CF', N'CAF', CAST(6.571493534252980 AS Decimal(20, 15)), CAST(20.482819355987100 AS Decimal(20, 15)), N'AFRO'),
    (N'Canada', N'CA', N'CAN', CAST(61.393108309762400 AS Decimal(20, 15)), CAST(-98.260912560238400 AS Decimal(20, 15)), N'AMRO'),
    (N'Australia', N'AU', N'AUS', CAST(-25.733279753479800 AS Decimal(20, 15)), CAST(134.491115119519000 AS Decimal(20, 15)), N'WPRO'),
    (N'Chile', N'CL', N'CHL', CAST(-37.868098222266000 AS Decimal(20, 15)), CAST(-71.380058599784900 AS Decimal(20, 15)), N'AMRO'),
    (N'China', N'CN', N'CHN', CAST(36.518747399862300 AS Decimal(20, 15)), CAST(103.893513649499000 AS Decimal(20, 15)), N'WPRO'),
    (N'Cameroon', N'CM', N'CMR', CAST(5.685915078407220 AS Decimal(20, 15)), CAST(12.743270887506900 AS Decimal(20, 15)), N'AFRO'),
    (N'New Zealand', N'NZ', N'NZL', CAST(-41.837071107868000 AS Decimal(20, 15)), CAST(171.604128106943000 AS Decimal(20, 15)), N'WPRO'),
    (N'Colombia', N'CO', N'COL', CAST(3.900414434665770 AS Decimal(20, 15)), CAST(-73.075716663786500 AS Decimal(20, 15)), N'AMRO'),
    (N'Comoros', N'KM', N'COM', CAST(-11.893318122218900 AS Decimal(20, 15)), CAST(43.676157552107100 AS Decimal(20, 15)), N'AFRO'),
    (N'Cabo Verde', N'CV', N'CPV', CAST(15.958641947190900 AS Decimal(20, 15)), CAST(-23.986310553036800 AS Decimal(20, 15)), N'AFRO'),
    (N'Costa Rica', N'CR', N'CRI', CAST(9.970529289890110 AS Decimal(20, 15)), CAST(-84.188958065890100 AS Decimal(20, 15)), N'AMRO'),
    (N'Cuba', N'CU', N'CUB', CAST(21.621472517724800 AS Decimal(20, 15)), CAST(-79.038152822933200 AS Decimal(20, 15)), N'AMRO'),
    (N'Djibouti', N'DJ', N'DJI', CAST(11.749852852974500 AS Decimal(20, 15)), CAST(42.577987884862800 AS Decimal(20, 15)), N'EMRO'),
    (N'Dominica', N'DM', N'DMA', CAST(15.436677293315500 AS Decimal(20, 15)), CAST(-61.355457598982200 AS Decimal(20, 15)), N'AMRO'),
    (N'Dominican Republic', N'DO', N'DOM', CAST(18.893146675465800 AS Decimal(20, 15)), CAST(-70.485409073557200 AS Decimal(20, 15)), N'AMRO'),
    (N'Algeria', N'DZ', N'DZA', CAST(28.163394227159100 AS Decimal(20, 15)), CAST(2.632402140453960 AS Decimal(20, 15)), N'AFRO'),
    (N'Ecuador', N'EC', N'ECU', CAST(-1.425243172026650 AS Decimal(20, 15)), CAST(-78.780889339727700 AS Decimal(20, 15)), N'AMRO'),
    (N'Egypt', N'EG', N'EGY', CAST(26.556740375666500 AS Decimal(20, 15)), CAST(29.782272588506800 AS Decimal(20, 15)), N'EMRO'),
    (N'Eritrea', N'ER', N'ERI', CAST(15.358891672540600 AS Decimal(20, 15)), CAST(38.852991671469300 AS Decimal(20, 15)), N'AFRO'),
    (N'Western Sahara', N'EH', N'ESH', CAST(24.661626710114400 AS Decimal(20, 15)), CAST(-13.136703858403500 AS Decimal(20, 15)), N'AFRO'),
    (N'Ethiopia', N'ET', N'ETH', CAST(8.626186842661750 AS Decimal(20, 15)), CAST(39.616085735496400 AS Decimal(20, 15)), N'AFRO'),
    (N'Fiji', N'FJ', N'FJI', CAST(-17.463901242611000 AS Decimal(20, 15)), CAST(160.784119780872000 AS Decimal(20, 15)), N'WPRO'),
    (N'Federated States of Micronesia', N'FM', N'FSM', CAST(6.911667 AS Decimal(20, 15)), CAST(158.141944 AS Decimal(20, 15)), N'WPRO'),
    (N'Gabon', N'GA', N'GAB', CAST(-0.590669623908125 AS Decimal(20, 15)), CAST(11.797226217476100 AS Decimal(20, 15)), N'AFRO'),
    (N'Ghana', N'GH', N'GHA', CAST(7.959686546712390 AS Decimal(20, 15)), CAST(-1.207010652652700 AS Decimal(20, 15)), N'AFRO'),
    (N'Guinea', N'GN', N'GIN', CAST(10.434562465953500 AS Decimal(20, 15)), CAST(-10.937465030326000 AS Decimal(20, 15)), N'AFRO'),
    (N'Gambia', N'GM', N'GMB', CAST(13.452985340149700 AS Decimal(20, 15)), CAST(-15.385645853286500 AS Decimal(20, 15)), N'AFRO'),
    (N'Guinea-Bissau', N'GW', N'GNB', CAST(12.013246633181200 AS Decimal(20, 15)), CAST(-14.986501934922700 AS Decimal(20, 15)), N'AFRO'),
    (N'Equatorial Guinea', N'GQ', N'GNQ', CAST(1.710206582738880 AS Decimal(20, 15)), CAST(10.338533954614800 AS Decimal(20, 15)), N'AFRO'),
    (N'Grenada', N'GD', N'GRD', CAST(12.162507229148000 AS Decimal(20, 15)), CAST(-61.650023881266400 AS Decimal(20, 15)), N'AMRO'),
    (N'Guatemala', N'GT', N'GTM', CAST(15.702344085345800 AS Decimal(20, 15)), CAST(-90.356921201734500 AS Decimal(20, 15)), N'AMRO'),
    (N'Guyana', N'GY', N'GUY', CAST(4.792428585028980 AS Decimal(20, 15)), CAST(-58.974132736994600 AS Decimal(20, 15)), N'AMRO'),
    (N'Honduras', N'HN', N'HND', CAST(14.819403479045000 AS Decimal(20, 15)), CAST(-86.619616019838700 AS Decimal(20, 15)), N'AMRO'),
    (N'Haiti', N'HT', N'HTI', CAST(18.939036204717100 AS Decimal(20, 15)), CAST(-72.683544995659500 AS Decimal(20, 15)), N'AMRO'),
    (N'Indonesia', N'ID', N'IDN', CAST(-2.228998493424840 AS Decimal(20, 15)), CAST(117.311053162642000 AS Decimal(20, 15)), N'SEARO'),
    (N'Islamic Republic of Iran', N'IR', N'IRN', CAST(32.427908 AS Decimal(20, 15)), CAST(53.688046 AS Decimal(20, 15)), N'EMRO'),
    (N'Iraq', N'IQ', N'IRQ', CAST(33.048056694929500 AS Decimal(20, 15)), CAST(43.772291333380300 AS Decimal(20, 15)), N'EMRO'),
    (N'Jamaica', N'JM', N'JAM', CAST(18.151375941702400 AS Decimal(20, 15)), CAST(-77.319714872804800 AS Decimal(20, 15)), N'AMRO'),
    (N'Jordan', N'JO', N'JOR', CAST(31.253431417528300 AS Decimal(20, 15)), CAST(36.786991642586800 AS Decimal(20, 15)), N'EMRO'),
    (N'Japan', N'JP', N'JPN', CAST(37.539826453615100 AS Decimal(20, 15)), CAST(137.973873099098000 AS Decimal(20, 15)), N'WPRO'),
    (N'Kenya', N'KE', N'KEN', CAST(0.530073960833084 AS Decimal(20, 15)), CAST(37.857844345607100 AS Decimal(20, 15)), N'AFRO'),
    (N'Cambodia', N'KH', N'KHM', CAST(12.714083901773100 AS Decimal(20, 15)), CAST(104.921277320518000 AS Decimal(20, 15)), N'WPRO'),
    (N'Kiribati', N'KI', N'KIR', CAST(0.427049958189763 AS Decimal(20, 15)), CAST(-50.512938624650000 AS Decimal(20, 15)), N'WPRO'),
    (N'Saint Kitts and Nevis', N'KN', N'KNA', CAST(17.265886051022000 AS Decimal(20, 15)), CAST(-62.696133129251000 AS Decimal(20, 15)), N'AMRO'),
    (N'Republic of Korea', N'KR', N'KOR', CAST(36.369576603460900 AS Decimal(20, 15)), CAST(127.824714210601000 AS Decimal(20, 15)), N'WPRO'),
    (N'Kuwait', N'KW', N'KWT', CAST(29.342676020301500 AS Decimal(20, 15)), CAST(47.593808822587200 AS Decimal(20, 15)), N'EMRO'),
    (N'Lao People''s Democratic Republic', N'LA', N'LAO', CAST(17.966556 AS Decimal(20, 15)), CAST(102.600000 AS Decimal(20, 15)), N'WPRO'),
    (N'Lebanon', N'LB', N'LBN', CAST(33.920495278648400 AS Decimal(20, 15)), CAST(35.888149119710500 AS Decimal(20, 15)), N'EMRO'),
    (N'Liberia', N'LR', N'LBR', CAST(6.446843098448420 AS Decimal(20, 15)), CAST(-9.306570132613150 AS Decimal(20, 15)), N'AFRO'),
    (N'Libya', N'LY', N'LBY', CAST(27.044098126037200 AS Decimal(20, 15)), CAST(18.023248081871700 AS Decimal(20, 15)), N'EMRO'),
    (N'Saint Lucia', N'LC', N'LCA', CAST(13.898131341828600 AS Decimal(20, 15)), CAST(-60.968661411220700 AS Decimal(20, 15)), N'AMRO'),
    (N'Sri Lanka', N'LK', N'LKA', CAST(7.612398634433770 AS Decimal(20, 15)), CAST(80.704319564866900 AS Decimal(20, 15)), N'SEARO'),
    (N'Lesotho', N'LS', N'LSO', CAST(-29.580982100555100 AS Decimal(20, 15)), CAST(28.242891459389200 AS Decimal(20, 15)), N'AFRO'),
    (N'Morocco', N'MA', N'MAR', CAST(31.883579992545700 AS Decimal(20, 15)), CAST(-6.317718483298890 AS Decimal(20, 15)), N'EMRO'),
    (N'Madagascar', N'MG', N'MDG', CAST(-19.373512403892700 AS Decimal(20, 15)), CAST(46.706037383743200 AS Decimal(20, 15)), N'AFRO'),
    (N'Maldives', N'MV', N'MDV', CAST(3.370332562262030 AS Decimal(20, 15)), CAST(73.252596650604300 AS Decimal(20, 15)), N'SEARO'),
    (N'Mexico', N'MX', N'MEX', CAST(23.950978693147600 AS Decimal(20, 15)), CAST(-102.534962805249000 AS Decimal(20, 15)), N'AMRO'),
    (N'Marshall Islands', N'MH', N'MHL', CAST(8.984003427679960 AS Decimal(20, 15)), CAST(167.781075250332000 AS Decimal(20, 15)), N'WPRO'),
    (N'Mali', N'ML', N'MLI', CAST(17.349040132962800 AS Decimal(20, 15)), CAST(-3.525618463933750 AS Decimal(20, 15)), N'AFRO'),
    (N'Myanmar', N'MM', N'MMR', CAST(21.140205252612300 AS Decimal(20, 15)), CAST(96.508626347999500 AS Decimal(20, 15)), N'SEARO'),
    (N'Molia', N'MN', N'MNG', CAST(46.835261667280100 AS Decimal(20, 15)), CAST(103.083093027933000 AS Decimal(20, 15)), N'WPRO'),
    (N'Mozambique', N'MZ', N'MOZ', CAST(-17.260173626099000 AS Decimal(20, 15)), CAST(35.552322811659100 AS Decimal(20, 15)), N'AFRO'),
    (N'Mauritania', N'MR', N'MRT', CAST(20.259951151750700 AS Decimal(20, 15)), CAST(-10.332232888847800 AS Decimal(20, 15)), N'AFRO'),
    (N'Mauritius', N'MU', N'MUS', CAST(-20.170461615372900 AS Decimal(20, 15)), CAST(57.868432475050400 AS Decimal(20, 15)), N'AFRO'),
    (N'Malawi', N'MW', N'MWI', CAST(-13.214998046188900 AS Decimal(20, 15)), CAST(34.307010265996200 AS Decimal(20, 15)), N'AFRO'),
    (N'Malaysia', N'MY', N'MYS', CAST(3.793875844633480 AS Decimal(20, 15)), CAST(109.711591992851000 AS Decimal(20, 15)), N'WPRO'),
    (N'Namibia', N'NA', N'NAM', CAST(-22.133274233860700 AS Decimal(20, 15)), CAST(17.218495327068700 AS Decimal(20, 15)), N'AFRO'),
    (N'Niger', N'NE', N'NER', CAST(17.426382711653700 AS Decimal(20, 15)), CAST(9.397717445010010 AS Decimal(20, 15)), N'AFRO'),
    (N'Nigeria', N'NG', N'NGA', CAST(9.593641659187880 AS Decimal(20, 15)), CAST(8.105324365068640 AS Decimal(20, 15)), N'AFRO'),
    (N'Nicaragua', N'NI', N'NIC', CAST(12.840107277900400 AS Decimal(20, 15)), CAST(-85.033763323359700 AS Decimal(20, 15)), N'AMRO'),
    (N'Nepal', N'NP', N'NPL', CAST(28.253057175537200 AS Decimal(20, 15)), CAST(83.938560934176000 AS Decimal(20, 15)), N'SEARO'),
    (N'Oman', N'OM', N'OMN', CAST(20.602138481541200 AS Decimal(20, 15)), CAST(56.109874913080800 AS Decimal(20, 15)), N'EMRO'),
    (N'Pakistan', N'PK', N'PAK', CAST(29.966213780133500 AS Decimal(20, 15)), CAST(69.384952038149700 AS Decimal(20, 15)), N'EMRO'),
    (N'Panama', N'PA', N'PAN', CAST(8.505469360168560 AS Decimal(20, 15)), CAST(-80.108973290092900 AS Decimal(20, 15)), N'AMRO'),
    (N'Peru', N'PE', N'PER', CAST(-9.163753194119660 AS Decimal(20, 15)), CAST(-74.375365413796000 AS Decimal(20, 15)), N'AMRO'),
    (N'Philippines', N'PH', N'PHL', CAST(11.747465287587100 AS Decimal(20, 15)), CAST(122.873669337459000 AS Decimal(20, 15)), N'WPRO'),
    (N'Democratic People''s Republic of Korea', N'KP', N'PRK', CAST(40.339852 AS Decimal(20, 15)), CAST(127.510093 AS Decimal(20, 15)), N'WPRO'),
    (N'Paraguay', N'PY', N'PRY', CAST(-23.236098554176000 AS Decimal(20, 15)), CAST(-58.390922609867600 AS Decimal(20, 15)), N'AMRO'),
    (N'occupied Palestinian territory, including east Jerusalem', N'PS', N'PSE', CAST(31.913958654178400 AS Decimal(20, 15)), CAST(35.203787592285800 AS Decimal(20, 15)), N'EMRO'),
    (N'Qatar', N'QA', N'QAT', CAST(25.315851713491600 AS Decimal(20, 15)), CAST(51.191264024174200 AS Decimal(20, 15)), N'EMRO'),
    (N'Rwanda', N'RW', N'RWA', CAST(-1.997951266037070 AS Decimal(20, 15)), CAST(29.917236804451100 AS Decimal(20, 15)), N'AFRO'),
    (N'Saudi Arabia', N'SA', N'SAU', CAST(24.080959495648200 AS Decimal(20, 15)), CAST(44.570482919618000 AS Decimal(20, 15)), N'EMRO'),
    (N'Sudan', N'SD', N'SDN', CAST(16.048851908374800 AS Decimal(20, 15)), CAST(30.004409459698200 AS Decimal(20, 15)), N'EMRO'),
    (N'Senegal', N'SN', N'SEN', CAST(14.367358906806600 AS Decimal(20, 15)), CAST(-14.468945861297800 AS Decimal(20, 15)), N'AFRO'),
    (N'Singapore', N'SG', N'SGP', CAST(1.351246754630750 AS Decimal(20, 15)), CAST(103.807708909403000 AS Decimal(20, 15)), N'WPRO'),
    (N'Solomon Islands', N'SB', N'SLB', CAST(-8.904243001061380 AS Decimal(20, 15)), CAST(159.612778248811000 AS Decimal(20, 15)), N'WPRO'),
    (N'Sierra Leone', N'SL', N'SLE', CAST(8.558572452789800 AS Decimal(20, 15)), CAST(-11.792370940656200 AS Decimal(20, 15)), N'AFRO'),
    (N'El Salvador', N'SV', N'SLV', CAST(13.736184594763100 AS Decimal(20, 15)), CAST(-88.865577059975700 AS Decimal(20, 15)), N'AMRO'),
    (N'Somalia', N'SO', N'SOM', CAST(6.063781454350310 AS Decimal(20, 15)), CAST(45.862630137215500 AS Decimal(20, 15)), N'EMRO'),
    (N'South Sudan', N'SS', N'SSD', CAST(7.279644976615840 AS Decimal(20, 15)), CAST(30.340757511507000 AS Decimal(20, 15)), N'AFRO'),
    (N'Sao Tome and Principe', N'ST', N'STP', CAST(0.435938941444158 AS Decimal(20, 15)), CAST(6.716183130808990 AS Decimal(20, 15)), N'AFRO'),
    (N'Suriname', N'SR', N'SUR', CAST(4.126752587657780 AS Decimal(20, 15)), CAST(-55.911560456541000 AS Decimal(20, 15)), N'AMRO'),
    (N'Eswatini', N'SZ', N'SWZ', CAST(-26.562096141305300 AS Decimal(20, 15)), CAST(31.497227974653000 AS Decimal(20, 15)), N'AFRO'),
    (N'Seychelles', N'SC', N'SYC', CAST(-6.398255476900570 AS Decimal(20, 15)), CAST(52.232074888214600 AS Decimal(20, 15)), N'AFRO'),
    (N'Syrian Arab Republic', N'SY', N'SYR', CAST(35.013127005761800 AS Decimal(20, 15)), CAST(38.505912046423400 AS Decimal(20, 15)), N'EMRO'),
    (N'Chad', N'TD', N'TCD', CAST(15.361188210825200 AS Decimal(20, 15)), CAST(18.664645883490800 AS Decimal(20, 15)), N'AFRO'),
    (N'Thailand', N'TH', N'THA', CAST(15.119946831021900 AS Decimal(20, 15)), CAST(101.014952093685000 AS Decimal(20, 15)), N'SEARO'),
    (N'Timor-Leste', N'TL', N'TLS', CAST(-8.820512127767230 AS Decimal(20, 15)), CAST(125.861026989377000 AS Decimal(20, 15)), N'SEARO'),
    (N'Tonga', N'TO', N'TON', CAST(-19.862799090378100 AS Decimal(20, 15)), CAST(-174.828856371341000 AS Decimal(20, 15)), N'WPRO'),
    (N'Trinidad and Tob', N'TT', N'TTO', CAST(10.468417324861500 AS Decimal(20, 15)), CAST(-61.252626180047000 AS Decimal(20, 15)), N'AMRO'),
    (N'Tunisia', N'TN', N'TUN', CAST(34.111309988763400 AS Decimal(20, 15)), CAST(9.561613971967190 AS Decimal(20, 15)), N'EMRO'),
    (N'Tuvalu', N'TV', N'TUV', CAST(-7.633892202406500 AS Decimal(20, 15)), CAST(178.232463990749000 AS Decimal(20, 15)), N'WPRO'),
    (N'United Republic of Tanzania', N'TZ', N'TZA', CAST(-6.270159743712280 AS Decimal(20, 15)), CAST(34.823443818710400 AS Decimal(20, 15)), N'AFRO'),
    (N'Uganda', N'UG', N'UGA', CAST(1.280074673097730 AS Decimal(20, 15)), CAST(32.386320497965900 AS Decimal(20, 15)), N'AFRO'),
    (N'Uruguay', N'UY', N'URY', CAST(-32.799536141379700 AS Decimal(20, 15)), CAST(-56.012144435232900 AS Decimal(20, 15)), N'AMRO'),
    (N'Saint Vincent and the Grenadines', N'VC', N'VCT', CAST(13.202429888881600 AS Decimal(20, 15)), CAST(-61.207415861854800 AS Decimal(20, 15)), N'AMRO'),
    (N'Bolivarian Republic of Venezuela', N'VE', N'VEN', CAST(6.423758 AS Decimal(20, 15)), -CAST(66.589730 AS Decimal(20, 15)), N'AMRO'),
    (N'Viet Nam', N'VN', N'VNM', CAST(16.651666554520600 AS Decimal(20, 15)), CAST(106.304074627029000 AS Decimal(20, 15)), N'WPRO'),
    (N'Vanuatu', N'VU', N'VUT', CAST(-16.202119815012700 AS Decimal(20, 15)), CAST(167.705619686079000 AS Decimal(20, 15)), N'WPRO'),
    (N'Samoa', N'WS', N'WSM', CAST(-13.758324837658700 AS Decimal(20, 15)), CAST(-172.159412241517000 AS Decimal(20, 15)), N'WPRO'),
    (N'Yemen', N'YE', N'YEM', CAST(15.838711753081400 AS Decimal(20, 15)), CAST(47.582291774467500 AS Decimal(20, 15)), N'EMRO'),
    (N'South Africa', N'ZA', N'ZAF', CAST(-28.993185375955400 AS Decimal(20, 15)), CAST(25.088765158420200 AS Decimal(20, 15)), N'AFRO'),
    (N'Zambia', N'ZM', N'ZMB', CAST(-13.453055798296900 AS Decimal(20, 15)), CAST(27.797906377487500 AS Decimal(20, 15)), N'AFRO'),
    (N'Zimbabwe', N'ZW', N'ZWE', CAST(-18.999977182140100 AS Decimal(20, 15)), CAST(29.871814425367800 AS Decimal(20, 15)), N'AFRO'),
    (N'Papua New Guinea', N'PG', N'PNG', CAST(-6.476465467282180 AS Decimal(20, 15)), CAST(145.253085578970000 AS Decimal(20, 15)), N'WPRO'),
    (N'Cote d''Ivoire', N'CI', N'CIV', CAST(7.540101 AS Decimal(20, 15)), -CAST(5.547200 AS Decimal(20, 15)), N'AFRO'),
    (N'Albania', N'AL', N'ALB', CAST(41.146014537994000 AS Decimal(20, 15)), CAST(20.069232680267200 AS Decimal(20, 15)), N'EURO'),
    (N'Andorra', N'AD', N'AND', CAST(42.548670435909400 AS Decimal(20, 15)), CAST(1.575723958250430 AS Decimal(20, 15)), N'EURO'),
    (N'Armenia', N'AM', N'ARM', CAST(40.286462579465400 AS Decimal(20, 15)), CAST(44.947968918191500 AS Decimal(20, 15)), N'EURO'),
    (N'Austria', N'AT', N'AUT', CAST(47.593012391143800 AS Decimal(20, 15)), CAST(14.140050551807000 AS Decimal(20, 15)), N'EURO'),
    (N'Azerbaijan', N'AZ', N'AZE', CAST(40.292267841403600 AS Decimal(20, 15)), CAST(47.532390496506400 AS Decimal(20, 15)), N'EURO'),
    (N'Belgium', N'BE', N'BEL', CAST(50.642990004097900 AS Decimal(20, 15)), CAST(4.664426159758920 AS Decimal(20, 15)), N'EURO'),
    (N'Bulgaria', N'BG', N'BGR', CAST(42.760364277481200 AS Decimal(20, 15)), CAST(25.234813651288100 AS Decimal(20, 15)), N'EURO'),
    (N'Bosnia and Herzvina', N'BA', N'BIH', CAST(44.166499296051100 AS Decimal(20, 15)), CAST(17.788658352163500 AS Decimal(20, 15)), N'EURO'),
    (N'Belarus', N'BY', N'BLR', CAST(53.540269924606900 AS Decimal(20, 15)), CAST(28.047007399481200 AS Decimal(20, 15)), N'EURO'),
    (N'Switzerland', N'CH', N'CHE', CAST(46.802629408001300 AS Decimal(20, 15)), CAST(8.234635159599620 AS Decimal(20, 15)), N'EURO'),
    (N'Cyprus', N'CY', N'CYP', CAST(35.043040409471600 AS Decimal(20, 15)), CAST(33.218689858960700 AS Decimal(20, 15)), N'EURO'),
    (N'Czech Republic', N'CZ', N'CZE', CAST(49.817492 AS Decimal(20, 15)), CAST(15.472962 AS Decimal(20, 15)), N'EURO'),
    (N'Germany', N'DE', N'DEU', CAST(51.110202214173900 AS Decimal(20, 15)), CAST(10.392362087913800 AS Decimal(20, 15)), N'EURO'),
    (N'Denmark', N'DK', N'DNK', CAST(55.962366945844100 AS Decimal(20, 15)), CAST(10.050867245741900 AS Decimal(20, 15)), N'EURO'),
    (N'Spain', N'ES', N'ESP', CAST(40.227570403503200 AS Decimal(20, 15)), CAST(-3.649248588317870 AS Decimal(20, 15)), N'EURO'),
    (N'Estonia', N'EE', N'EST', CAST(58.673860640329100 AS Decimal(20, 15)), CAST(25.525269898985500 AS Decimal(20, 15)), N'EURO'),
    (N'Finland', N'FI', N'FIN', CAST(64.494383118936900 AS Decimal(20, 15)), CAST(26.258877263060100 AS Decimal(20, 15)), N'EURO'),
    (N'Georgia', N'GE', N'GEO', CAST(42.176365520666100 AS Decimal(20, 15)), CAST(43.517628235303700 AS Decimal(20, 15)), N'EURO'),
    (N'Greece', N'GR', N'GRC', CAST(39.044587785344400 AS Decimal(20, 15)), CAST(22.988032553382700 AS Decimal(20, 15)), N'EURO'),
    (N'Croatia', N'HR', N'HRV', CAST(45.043707605677700 AS Decimal(20, 15)), CAST(16.409402749836500 AS Decimal(20, 15)), N'EURO'),
    (N'Hungary', N'HU', N'HUN', CAST(47.166529575965300 AS Decimal(20, 15)), CAST(19.413506657106000 AS Decimal(20, 15)), N'EURO'),
    (N'Ireland', N'IE', N'IRL', CAST(53.177117555941700 AS Decimal(20, 15)), CAST(-8.151564618671800 AS Decimal(20, 15)), N'EURO'),
    (N'Iceland', N'IS', N'ISL', CAST(64.997696594349400 AS Decimal(20, 15)), CAST(-18.605671262027300 AS Decimal(20, 15)), N'EURO'),
    (N'Israel', N'IL', N'ISR', CAST(31.358096524242000 AS Decimal(20, 15)), CAST(34.966039954210000 AS Decimal(20, 15)), N'EURO'),
    (N'Italy', N'IT', N'ITA', CAST(42.791244392818000 AS Decimal(20, 15)), CAST(12.071713771949200 AS Decimal(20, 15)), N'EURO'),
    (N'Kazakhstan', N'KZ', N'KAZ', CAST(48.160093119810100 AS Decimal(20, 15)), CAST(67.301356763745300 AS Decimal(20, 15)), N'EURO'),
    (N'Liechtenstein', N'LI', N'LIE', CAST(47.152649446588800 AS Decimal(20, 15)), CAST(9.555345637888920 AS Decimal(20, 15)), N'EURO'),
    (N'Lithuania', N'LT', N'LTU', CAST(55.335900765320100 AS Decimal(20, 15)), CAST(23.896915728827400 AS Decimal(20, 15)), N'EURO'),
    (N'Luxembourg', N'LU', N'LUX', CAST(49.771016618519700 AS Decimal(20, 15)), CAST(6.087605976515080 AS Decimal(20, 15)), N'EURO'),
    (N'Latvia', N'LV', N'LVA', CAST(56.857718119353200 AS Decimal(20, 15)), CAST(24.929220014972100 AS Decimal(20, 15)), N'EURO'),
    (N'Monaco', N'MC', N'MCO', CAST(43.750425850315900 AS Decimal(20, 15)), CAST(7.412049410928510 AS Decimal(20, 15)), N'EURO'),
    (N'Republic of Moldova', N'MD', N'MDA', CAST(47.193376942472400 AS Decimal(20, 15)), CAST(28.474372865862000 AS Decimal(20, 15)), N'EURO'),
    (N'North Macedonia', N'MK', N'MKD', CAST(41.599293837189500 AS Decimal(20, 15)), CAST(21.699210985707000 AS Decimal(20, 15)), N'EURO'),
    (N'Malta', N'MT', N'MLT', CAST(35.920423034223800 AS Decimal(20, 15)), CAST(14.404629843563200 AS Decimal(20, 15)), N'EURO'),
    (N'Montenegro', N'ME', N'MNE', CAST(42.796276937927200 AS Decimal(20, 15)), CAST(19.264702813469800 AS Decimal(20, 15)), N'EURO'),
    (N'Norway', N'NO', N'NOR', CAST(64.445025655291500 AS Decimal(20, 15)), CAST(14.070356555279500 AS Decimal(20, 15)), N'EURO'),
    (N'Poland', N'PL', N'POL', CAST(52.124780568817700 AS Decimal(20, 15)), CAST(19.400956952774200 AS Decimal(20, 15)), N'EURO'),
    (N'Portugal', N'PT', N'PRT', CAST(39.602749400511600 AS Decimal(20, 15)), CAST(-8.467327074383410 AS Decimal(20, 15)), N'EURO'),
    (N'Romania', N'RO', N'ROU', CAST(45.843646421871400 AS Decimal(20, 15)), CAST(24.969960957956300 AS Decimal(20, 15)), N'EURO'),
    (N'Russian Federation', N'RU', N'RUS', CAST(61.988294531408800 AS Decimal(20, 15)), CAST(96.689487299952700 AS Decimal(20, 15)), N'EURO'),
    (N'San Marino', N'SM', N'SMR', CAST(43.941927850837400 AS Decimal(20, 15)), CAST(12.460457881399000 AS Decimal(20, 15)), N'EURO'),
    (N'Serbia', N'RS', N'SRB', CAST(44.033917230893700 AS Decimal(20, 15)), CAST(20.811383802358200 AS Decimal(20, 15)), N'EURO'),
    (N'Slovakia', N'SK', N'SVK', CAST(48.707416720593100 AS Decimal(20, 15)), CAST(19.491505221691500 AS Decimal(20, 15)), N'EURO'),
    (N'Slovenia', N'SI', N'SVN', CAST(46.123599981198300 AS Decimal(20, 15)), CAST(14.827161589612100 AS Decimal(20, 15)), N'EURO'),
    (N'Sweden', N'SE', N'SWE', CAST(62.787797772592100 AS Decimal(20, 15)), CAST(16.741200722499100 AS Decimal(20, 15)), N'EURO'),
    (N'Turkmenistan', N'TM', N'TKM', CAST(39.122264196414700 AS Decimal(20, 15)), CAST(59.383578546412400 AS Decimal(20, 15)), N'EURO'),
    (N'Türkiye', N'TR', N'TUR', CAST(39.060666457326500 AS Decimal(20, 15)), CAST(35.179254591994600 AS Decimal(20, 15)), N'EURO'),
    (N'Ukraine', N'UA', N'UKR', CAST(49.016145992121200 AS Decimal(20, 15)), CAST(31.387887611077200 AS Decimal(20, 15)), N'EURO'),
    (N'Holy See', N'VA', N'VAT', CAST(41.893249112692100 AS Decimal(20, 15)), CAST(12.485675797007500 AS Decimal(20, 15)), N'EURO'),
    (N'Kyrgyzstan', N'KG', N'KGZ', CAST(41.467755423992100 AS Decimal(20, 15)), CAST(74.562106337667500 AS Decimal(20, 15)), N'EURO'),
    (N'Tajikistan', N'TJ', N'TJK', CAST(38.529293471655800 AS Decimal(20, 15)), CAST(71.041851356170900 AS Decimal(20, 15)), N'EURO'),
    (N'Uzbekistan', N'UZ', N'UZB', CAST(41.749485520538100 AS Decimal(20, 15)), CAST(63.173936247694600 AS Decimal(20, 15)), N'EURO'),
    (N'Nauru', N'NR', N'NRU', CAST(-0.523352130853328 AS Decimal(20, 15)), CAST(166.920671828247000 AS Decimal(20, 15)), N'WPRO'),
    (N'Palau', N'PW', N'PLW', CAST(7.285774657312240 AS Decimal(20, 15)), CAST(134.400185205934000 AS Decimal(20, 15)), N'WPRO'),
    (N'Aruba', N'AW', N'ABW', CAST(12.516687322013600 AS Decimal(20, 15)), CAST(-69.977083594496500 AS Decimal(20, 15)), N'AMRO'),
    (N'Anguilla', N'AI', N'AIA', CAST(18.231988398962100 AS Decimal(20, 15)), CAST(-63.061565526423500 AS Decimal(20, 15)), N'AMRO'),
    (N'American Samoa', N'AS', N'ASM', CAST(-14.264258830775900 AS Decimal(20, 15)), CAST(-170.404067977347000 AS Decimal(20, 15)), N'WPRO'),
    (N'Bonaire, Sint Eustatius and Saba', N'BQ', N'BES', CAST(12.800068707482200 AS Decimal(20, 15)), CAST(-67.685398835813100 AS Decimal(20, 15)), N'AMRO'),
    (N'Saint Barthélemy', N'BL', N'BLM', CAST(17.905796842565100 AS Decimal(20, 15)), CAST(-62.833490443452600 AS Decimal(20, 15)), N'AMRO'),
    (N'Bermuda', N'BM', N'BMU', CAST(32.313678007603500 AS Decimal(20, 15)), CAST(-64.756124846606700 AS Decimal(20, 15)), N'AMRO'),
    (N'Bolivia (Plurinational State of)', N'BO', N'BOL', CAST(-16.715086502326100 AS Decimal(20, 15)), CAST(-64.670503554770300 AS Decimal(20, 15)), N'AMRO'),
    (N'Cocos (Keeling) Islands', N'CC', N'CCK', CAST(-12.171843959102800 AS Decimal(20, 15)), CAST(96.855683412709400 AS Decimal(20, 15)), N'WPRO'),
    (N'Cook Islands', N'CK', N'COK', CAST(-18.337635185194600 AS Decimal(20, 15)), CAST(-159.572706381707000 AS Decimal(20, 15)), N'WPRO'),
    (N'Curaçao', N'CW', N'CUW', CAST(12.187776832405200 AS Decimal(20, 15)), CAST(-68.967369306788200 AS Decimal(20, 15)), N'AMRO'),
    (N'Christmas Island', N'CX', N'CXR', CAST(-10.444115676631900 AS Decimal(20, 15)), CAST(105.703736329417000 AS Decimal(20, 15)), N'WPRO'),
    (N'Cayman Islands', N'KY', N'CYM', CAST(19.427425358885700 AS Decimal(20, 15)), CAST(-80.867101558218200 AS Decimal(20, 15)), N'AMRO'),
    (N'Falkland Islands (Malvinas)', N'FK', N'FLK', CAST(-51.739776912949700 AS Decimal(20, 15)), CAST(-59.372320201158900 AS Decimal(20, 15)), N'AMRO'),
    (N'Micronesia (Federated States of)', N'FM', N'FSM', CAST(7.096635578605040 AS Decimal(20, 15)), CAST(154.821163671012000 AS Decimal(20, 15)), N'WPRO'),
    (N'Guadeloupe', N'GP', N'GLP', CAST(16.201378283718800 AS Decimal(20, 15)), CAST(-61.530334557584800 AS Decimal(20, 15)), N'AMRO'),
    (N'French Guiana', N'GF', N'GUF', CAST(3.924423458950710 AS Decimal(20, 15)), CAST(-53.241245305366800 AS Decimal(20, 15)), N'AMRO'),
    (N'Guam', N'GU', N'GUM', CAST(13.444093809845600 AS Decimal(20, 15)), CAST(144.775931314003000 AS Decimal(20, 15)), N'WPRO'),
    (N'Saint Martin', N'MF', N'MAF', CAST(18.087671827517100 AS Decimal(20, 15)), CAST(-63.064624708221500 AS Decimal(20, 15)), N'AMRO'),
    (N'Northern Mariana Islands (Commonwealth of the)', N'MP', N'MNP', CAST(16.255911164225000 AS Decimal(20, 15)), CAST(145.587959022428000 AS Decimal(20, 15)), N'WPRO'),
    (N'Montserrat', N'MS', N'MSR', CAST(16.735816385466200 AS Decimal(20, 15)), CAST(-62.186897467911300 AS Decimal(20, 15)), N'AMRO'),
    (N'Martinique', N'MQ', N'MTQ', CAST(14.652862600766500 AS Decimal(20, 15)), CAST(-61.021462067478200 AS Decimal(20, 15)), N'AMRO'),
    (N'Mayotte', N'YT', N'MYT', CAST(-12.818337501828800 AS Decimal(20, 15)), CAST(45.139302368364700 AS Decimal(20, 15)), N'AFRO'),
    (N'New Caledonia', N'NC', N'NCL', CAST(-21.294603601298300 AS Decimal(20, 15)), CAST(165.671878659330000 AS Decimal(20, 15)), N'WPRO'),
    (N'Norfolk Island', N'NF', N'NFK', CAST(-29.037141476619900 AS Decimal(20, 15)), CAST(167.952902820392000 AS Decimal(20, 15)), N'WPRO'),
    (N'Niue', N'NU', N'NIU', CAST(-19.037233290177800 AS Decimal(20, 15)), CAST(-169.881681776879000 AS Decimal(20, 15)), N'WPRO'),
    (N'Pitcairn Islands', N'PN', N'PCN', CAST(-23.991064665714300 AS Decimal(20, 15)), CAST(-129.718002811205000 AS Decimal(20, 15)), N'WPRO'),
    (N'Puerto Rico', N'PR', N'PRI', CAST(18.220981426253300 AS Decimal(20, 15)), CAST(-66.466080230898900 AS Decimal(20, 15)), N'AMRO'),
    (N'French Polynesia', N'PF', N'PYF', CAST(-15.098375221659500 AS Decimal(20, 15)), CAST(-145.529597925801000 AS Decimal(20, 15)), N'WPRO'),
    (N'Réunion', N'RE', N'REU', CAST(-21.121617360811700 AS Decimal(20, 15)), CAST(55.538254909572700 AS Decimal(20, 15)), N'AFRO'),
    (N'Saint Helena', N'SH', N'SHN', CAST(-25.069785018805100 AS Decimal(20, 15)), CAST(-10.204650940378700 AS Decimal(20, 15)), N'AFRO'),
    (N'Saint Pierre and Miquelon', N'PM', N'SPM', CAST(46.928953119577400 AS Decimal(20, 15)), CAST(-56.305254422456400 AS Decimal(20, 15)), N'AMRO'),
    (N'Sint Maarten', N'SX', N'SXM', CAST(18.046906972656200 AS Decimal(20, 15)), CAST(-63.061876333996700 AS Decimal(20, 15)), N'AMRO'),
    (N'Turks and Caicos Islands', N'TC', N'TCA', CAST(21.778363814824200 AS Decimal(20, 15)), CAST(-71.844947606183200 AS Decimal(20, 15)), N'AMRO'),
    (N'Venezuela (Bolivarian Republic of)', N'VE', N'VEN', CAST(7.124989765469340 AS Decimal(20, 15)), CAST(-66.166180826509100 AS Decimal(20, 15)), N'AMRO'),
    (N'British Virgin Islands', N'VG', N'VGB', CAST(18.477547352953600 AS Decimal(20, 15)), CAST(-64.511111435197000 AS Decimal(20, 15)), N'AMRO'),
    (N'United States Virgin Islands', N'VI', N'VIR', CAST(17.968020963873100 AS Decimal(20, 15)), CAST(-64.786850494459300 AS Decimal(20, 15)), N'AMRO'),
    (N'Wallis and Futuna', N'WF', N'WLF', CAST(-13.783899743166300 AS Decimal(20, 15)), CAST(-177.143458883888000 AS Decimal(20, 15)), N'WPRO'),
    (N'Johnston Atoll', N'JT', N'JTN', CAST(16.728750751013000 AS Decimal(20, 15)), CAST(-169.533246549525000 AS Decimal(20, 15)), N'WPRO'),
    (N'Midway Islands', N'UM', N'MID', CAST(28.205030845320600 AS Decimal(20, 15)), CAST(-177.366344324669000 AS Decimal(20, 15)), N'WPRO'),
    (N'Wake Island', N'WK', N'WAK', CAST(19.301598444756900 AS Decimal(20, 15)), CAST(166.638255736805000 AS Decimal(20, 15)), N'WPRO'),
    (N'Czechia', N'CZ', N'CZE', CAST(49.743082740871200 AS Decimal(20, 15)), CAST(15.338385885765700 AS Decimal(20, 15)), N'EURO'),
    (N'Faroe Islands', N'FO', N'FRO', CAST(62.050340640305900 AS Decimal(20, 15)), CAST(-6.863604557046700 AS Decimal(20, 15)), N'EURO'),
    (N'The United Kingdom', N'GB', N'GBR', CAST(54.160231890098300 AS Decimal(20, 15)), CAST(-2.900464772240240 AS Decimal(20, 15)), N'EURO'),
    (N'Guernsey', N'GG', N'GGY', CAST(49.459063845441300 AS Decimal(20, 15)), CAST(-2.575818128110590 AS Decimal(20, 15)), N'EURO'),
    (N'Gibraltar', N'GI', N'GIB', CAST(36.138179253758800 AS Decimal(20, 15)), CAST(-5.344872044673680 AS Decimal(20, 15)), N'EURO'),
    (N'Greenland', N'GL', N'GRL', CAST(74.719001146013200 AS Decimal(20, 15)), CAST(-41.390944421559900 AS Decimal(20, 15)), N'EURO'),
    (N'Isle of Man', N'IM', N'IMN', CAST(54.228856426163200 AS Decimal(20, 15)), CAST(-4.526552638344220 AS Decimal(20, 15)), N'EURO'),
    (N'Jersey', N'JE', N'JEY', CAST(49.219537522396300 AS Decimal(20, 15)), CAST(-2.128936796281480 AS Decimal(20, 15)), N'EURO'),
    (N'Svalbard and Jan Mayen Islands', N'SJ', N'SJM', CAST(78.829693371033600 AS Decimal(20, 15)), CAST(18.374472941802600 AS Decimal(20, 15)), N'EURO'),
    (N'Kosovo', N'XK', N'XKX', CAST(42.581211295831000 AS Decimal(20, 15)), CAST(20.898009280627000 AS Decimal(20, 15)), N'EURO'),
    (N'Tokelau', N'TK', N'TKL', CAST(-9.024063847147180 AS Decimal(20, 15)), CAST(-171.921967420053000 AS Decimal(20, 15)), N'WPRO')
END

-- -- Insert a user.
-- IF NOT EXISTS(Select *
-- FROM [dbo].[Users]
-- WHERE [Email] = 'dabhip@who.int')
-- BEGIN
--     INSERT INTO [dbo].[Users]
--         ([FirstName],[LastName],[Email],[ProfilePicture],[CountryId], [Region],[PreferredLanguageId]
--         ,[Institution],[Affiliation],[CreatedAt],[CreatedBy],[LastUpdatedAt],[LastUpdatedBy]
--         ,[Status], [DeactivationRequest],[IsReadOnly],[IsActive],[IsDeleted])
--     VALUES
--         ('Pratik', 'Dabhi', 'dabhip@who.int', NULL, NULL, NULL, 3
--         , 'test', 'test', GETUTCDATE(), 1, GETUTCDATE(), 1,
--             null, 0, 0, 1, 0)

--     INSERT INTO [dbo].[UserRoles]
--         ([UserId], [RoleId])
--     VALUES
--         (SCOPE_IDENTITY(), 1)
-- END

-- -- Insert a user.
-- IF NOT EXISTS(Select *
-- FROM [dbo].[Users]
-- WHERE [Email] = 'shuklash@who.int')
-- BEGIN
--     INSERT INTO [dbo].[Users]
--         ([FirstName],[LastName],[Email],[ProfilePicture],[CountryId], [Region],[PreferredLanguageId]
--         ,[Institution],[Affiliation],[CreatedAt],[CreatedBy],[LastUpdatedAt],[LastUpdatedBy]
--         ,[Status], [DeactivationRequest],[IsReadOnly],[IsActive],[IsDeleted])
--     VALUES
--         ('Shaishav', 'Shukla', 'shuklash@who.int', NULL, NULL, NULL, 3
--         , 'test', 'test', GETUTCDATE(), 1, GETUTCDATE(), 1,
--             null, 0, 0, 1, 0)

--     INSERT INTO [dbo].[UserRoles]
--         ([UserId], [RoleId])
--     VALUES
--         (SCOPE_IDENTITY(), 1)
-- END

-- -- Insert a user.
-- IF NOT EXISTS(Select *
-- FROM [dbo].[Users]
-- WHERE [Email] = 'ankurk@who.int')
-- BEGIN
--     INSERT INTO [dbo].[Users]
--         ([FirstName],[LastName],[Email],[ProfilePicture],[CountryId], [Region],[PreferredLanguageId]
--         ,[Institution],[Affiliation],[CreatedAt],[CreatedBy],[LastUpdatedAt],[LastUpdatedBy]
--         ,[Status], [DeactivationRequest],[IsReadOnly],[IsActive],[IsDeleted])
--     VALUES
--         ('Ankur', 'Khunt', 'ankurk@who.int', NULL, NULL, NULL, 3
--         , 'test', 'test', GETUTCDATE(), 1, GETUTCDATE(), 1,
--             null, 0, 0, 1, 0)

--     INSERT INTO [dbo].[UserRoles]
--         ([UserId], [RoleId])
--     VALUES
--         (SCOPE_IDENTITY(), 1)
-- END

-- -- Insert a user.
-- IF NOT EXISTS(Select *
-- FROM [dbo].[Users]
-- WHERE [Email] = 'sethyp@who.int')
-- BEGIN
--     INSERT INTO [dbo].[Users]
--         ([FirstName],[LastName],[Email],[ProfilePicture],[CountryId], [Region],[PreferredLanguageId]
--         ,[Institution],[Affiliation],[CreatedAt],[CreatedBy],[LastUpdatedAt],[LastUpdatedBy]
--         ,[Status], [DeactivationRequest],[IsReadOnly],[IsActive],[IsDeleted])
--     VALUES
--         ('Paresh', 'Sethy', 'sethyp@who.int', NULL, NULL, NULL, 3
--         , 'test', 'test', GETUTCDATE(), 1, GETUTCDATE(), 1,
--             null, 0, 0, 1, 0)

--     INSERT INTO [dbo].[UserRoles]
--         ([UserId], [RoleId])
--     VALUES
--         (SCOPE_IDENTITY(), 1)
-- END

-- -- Insert a user.
-- IF NOT EXISTS(Select *
-- FROM [dbo].[Users]
-- WHERE [Email] = 'pipadwalak@who.int')
-- BEGIN
--     INSERT INTO [dbo].[Users]
--         ([FirstName],[LastName],[Email],[ProfilePicture],[CountryId], [Region],[PreferredLanguageId]
--         ,[Institution],[Affiliation],[CreatedAt],[CreatedBy],[LastUpdatedAt],[LastUpdatedBy]
--         ,[Status], [DeactivationRequest],[IsReadOnly],[IsActive],[IsDeleted])
--     VALUES
--         ('Kaif', 'Pipadwala', 'pipadwalak@who.int', NULL, NULL, NULL, 3
--         , 'test', 'test', GETUTCDATE(), 1, GETUTCDATE(), 1,
--             null, 0, 0, 1, 0)

--     INSERT INTO [dbo].[UserRoles]
--         ([UserId], [RoleId])
--     VALUES
--         (SCOPE_IDENTITY(), 1)
-- END

-- IF (NOT EXISTS (SELECT TOP 1
--     1
-- FROM Sources))
-- BEGIN

--     INSERT [dbo].[Sources]
--         ([Name], [Description])
--     VALUES
--         (N'Custom', N'Assessment for SPAR Technical Areas and Indicators'),
--         (N'JEE-Assessment', N'Assessment for JEE Technical Areas and Indicators'),
--         (N'SPAR-Assessment', N'Assessment for SPAR Technical Areas and Indicators'),
--         (N'IHRBenchmarks', N'Assessment for IHR Benchmarks and Indicators'),
--         (N'NBWAssessments', N'Assessment for NBW Technical Areas and Indicators')
-- END

-- IF (NOT EXISTS (SELECT TOP 1
--     1
-- FROM CommonTechnicalAreas))
-- BEGIN

--     INSERT [dbo].[CommonTechnicalAreas]
--         ([DisplayName], [IndicatorId], [OrderBy])
--     VALUES
--         (N'Chemical events ', 1, 1),
--         (N'Financing ', 2, 2),
--         (N'Food safety ', 3, 3),
--         (N'Health emergency management ', 4, 4),
--         (N'Health services provision ', 5, 5),
--         (N'Human resources ', 6, 6),
--         (N'IHR Coordination, National IHR Focal Point functions and advocacy ', 7, 7),
--         (N'Infection prevention and control (IPC), ', 8, 8),
--         (N'Laboratory ', 9, 9),
--         (N'Points of entry (PoEs), and border health ', 10, 10),
--         (N'Policy, legal and normative Instruments to implement IHR ', 11, 11),
--         (N'Radiation emergencies', 12, 12),
--         (N'Risk communication and community engagement (RCCE), ', 13, 13),
--         (N'Surveillance ', 14, 14),
--         (N'Zoonotic diseases ', 15, 15)
-- END

-- IF (NOT EXISTS (SELECT TOP 1 1 FROM TechnicalAreas))
-- BEGIN
--     INSERT [dbo].[TechnicalAreas] ([Name], [AreaCode], [AreaCodeId], [IsActive], [SourceId], [CountryId], [CommonTechnicalAreaId], [IsCustomTechnicalArea], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])    
--     VALUES 
--         (N'IHR Coordination, National IHR Focal Point functions and advocacy', N'C', N'2', 1, 2, NULL, 7, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Policy, Legal and normative Instruments to implement IHR', N'C', N'1', 1, 2, NULL, 11, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Financing', N'C', N'3', 1, 2, NULL, 2, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Laboratory', N'C', N'4', 1, 2, NULL, 9, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Surveillance', N'C', N'5', 1, 2, NULL, 14, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Human resources', N'C', N'6', 1, 2, NULL, 6, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Health emergency management', N'C', N'7', 1, 2, NULL, 4, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Health services provision', N'C', N'8', 1, 2, NULL, 5, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Infection prevention and control (IPC)', N'C', N'9', 1, 2, NULL, 8, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Risk communication and community engagement (RCCE)', N'C', N'10', 1, 2, NULL, 13, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Points of entry (PoEs) and border health', N'C', N'11', 1, 2, NULL, 10, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Zoonotic diseases', N'C', N'12', 1, 2, NULL, 15, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Food safety', N'C', N'13', 1, 2, NULL, 3, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Chemical events', N'C', N'14', 1, 2, NULL, 1, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Radiation emergencies', N'C', N'15', 1, 2, NULL, 12, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Legal instruments', N'P', N'1', 1, 1, NULL, 11, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Financing', N'P', N'2', 1, 1, NULL, 2, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'IHR coordination, national IHR Focal Point functions and advocacy', N'P', N'3', 1, 1, NULL, 7, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Antimicrobial resistance (AMR)', N'P', N'4', 1, 1, NULL, NULL, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Zoonotic disease', N'P', N'5', 1, 1, NULL, 15, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Food safety', N'P', N'6', 1, 1, NULL, 3, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Biosafety and biosecurity', N'P', N'7', 1, 1, NULL, NULL, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Immunization', N'P', N'8', 1, 1, NULL, NULL, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'National laboratory system', N'D', N'1', 1, 1, NULL, 9, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Surveillance', N'D', N'2', 1, 1, NULL, 14, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Human resources', N'D', N'3', 1, 1, NULL, 6, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Health emergency management', N'R', N'1', 1, 1, NULL, 4, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Linking public health and security authorities', N'R', N'2', 1, 1, NULL, NULL, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Health services provision', N'R', N'3', 1, 1, NULL, 5, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Infection prevention and control (IPC)', N'R', N'4', 1, 1, NULL, 8, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Risk communication and community engagement (RCCE)', N'R', N'5', 1, 1, NULL, 13, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Points of entry and border health', N'PoE', N'', 1, 1, NULL, 10, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Chemical events', N'CE', N'', 1, 1, NULL, 1, 0, GETUTCDATE(), 0, GETUTCDATE(), 0),
--         (N'Radiation emergencies', N'RE', N'', 1, 1, NULL, 12, 0, GETUTCDATE(), 0, GETUTCDATE(), 0)
-- END


-- IF (NOT EXISTS (SELECT TOP 1
--     1
-- FROM TechnicalAreaIndicators))
-- BEGIN

--     SET IDENTITY_INSERT [dbo].[TechnicalAreaIndicators] ON
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (1, N'Policy, legal and normative instruments', 1, N'C', N'1.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (2, N'Gender Equality in health emergencies', 1, N'C', N'1.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (3, N'National IHR Focal Point functions', 2, N'C', N'2.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (4, N'Multisectoral IHR coordination mechanisms', 2, N'C', N'2.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (5, N'Advocacy for IHR implementation', 2, N'C', N'2.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (6, N'Financing for IHR implementation', 3, N'C', N'3.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (7, N'Financing for Public Health Emergency Response', 3, N'C', N'3.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (8, N'Specimen referral and transport system', 4, N'C', N'4.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (9, N'Implementation of a laboratory biosafety and biosecurity regime', 4, N'C', N'4.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (10, N'Laboratory quality system', 4, N'C', N'4.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (11, N'Laboratory testing capacity modalities', 4, N'C', N'4.4', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (12, N'Effective national diagnostic network', 4, N'C', N'4.5', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (13, N'Early warning surveillance function', 5, N'C', N'5.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (14, N'Event management (i.e., verification, investigation, analysis, and dissemination of information)', 5, N'C', N'5.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (15, N'Human resources for implementation of IHR', 6, N'C', N'6.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (16, N'Workforce surge during a public health event', 6, N'C', N'6.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (17, N'Planning for health emergencies', 7, N'C', N'7.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (18, N'Management of health emergency response', 7, N'C', N'7.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (19, N'Emergency logistic and supply chain management', 7, N'C', N'7.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (20, N'Case management', 8, N'C', N'8.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (21, N'Utilization of health services', 8, N'C', N'8.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (22, N'Continuity of essential health services', 8, N'C', N'8.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (23, N'IPC programmes', 9, N'C', N'9.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (24, N'Health care-associated infections (HCAI) surveillance', 9, N'C', N'9.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (25, N'Safe environment in health facilities', 9, N'C', N'9.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (26, N'RCCE system for emergencies', 10, N'C', N'10.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (27, N'Risk communication', 10, N'C', N'10.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (28, N'Community engagement', 10, N'C', N'10.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (29, N'Core capacity requirements at all times for PoEs (airports, ports and ground crossings)', 11, N'C', N'11.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (30, N'Public health response at points of entry', 11, N'C', N'11.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (31, N'Risk-based approach to international travel-related measures', 11, N'C', N'11.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (32, N'One Health collaborative efforts across sectors on activities to address zoonoses', 12, N'C', N'12.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (33, N'Multisectoral collaboration mechanism for food safety events', 13, N'C', N'13.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (34, N'Resources for detection and alert', 14, N'C', N'14.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (35, N'Capacity and resources', 15, N'C', N'15.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (36, N'Legal instruments', 16, N'P', N'1.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (37, N'Gender equity and equality in health emergencies', 16, N'P', N'1.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (38, N'Financial resources for IHR implementation', 17, N'P', N'2.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (39, N'Financial resources for public health emergency response', 17, N'P', N'2.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (40, N'National IHR Focal Point functions', 18, N'P', N'3.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (41, N'Multisectoral coordination mechanisms', 18, N'P', N'3.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (42, N'Strategic planning for IHR, preparedness or health security', 18, N'P', N'3.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (43, N'Multisectoral coordination on AMR', 19, N'P', N'4.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (44, N'Surveillance of AMR', 19, N'P', N'4.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (45, N'Prevention of Multidrug Resistant Organism (MDRO)', 19, N'P', N'4.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (46, N'Optimal use of antimicrobial medicines in human health', 19, N'P', N'4.4', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (47, N'Optimal use of antimicrobial medicines in animal health and agriculture', 19, N'P', N'4.5', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (48, N'Surveillance of zoonotic diseases', 20, N'P', N'5.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (49, N'Responding to zoonotic diseases', 20, N'P', N'5.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (50, N'Sanitary animal production practices', 20, N'P', N'5.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (51, N'Surveillance of foodborne diseases and contamination', 21, N'P', N'6.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (52, N'Response and management of food safety emergencies', 21, N'P', N'6.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (53, N'Whole-ofvernment biosafety and biosecurity system in place for all sectors (including human, animal and agriculture facilities)', 22, N'P', N'7.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (54, N'Biosafety and biosecurity training and practices in all relevant sectors (including human, animal and agriculture)', 22, N'P', N'7.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (55, N'Vaccine coverage (measles) as part of national programme', 23, N'P', N'8.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (56, N'National vaccine access and delivery', 23, N'P', N'8.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (57, N'Mass vaccination for epidemics of VPDs', 23, N'P', N'8.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (58, N'Specimen referral and transport system', 24, N'D', N'1.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (59, N'Laboratory quality system', 24, N'D', N'1.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (60, N'Laboratory testing capacity modalities', 24, N'D', N'1.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (61, N'Effective national diagnostic network', 24, N'D', N'1.4', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (62, N'Early warning surveillance function', 25, N'D', N'2.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (63, N'Event verification and investigation', 25, N'D', N'2.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (64, N'Analysis and information sharing', 25, N'D', N'2.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (65, N'Multisectoral workforce strategy', 26, N'D', N'3.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (66, N'Human resources for implementation of IHR', 26, N'D', N'3.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (67, N'Workforce training', 26, N'D', N'3.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (68, N'Workforce surge during a public health event', 26, N'D', N'3.4', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (69, N'Emergency risk and readiness assessment', 27, N'R', N'1.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (70, N'Public health emergency operations center', 27, N'R', N'1.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (71, N'Management of health emergency response', 27, N'R', N'1.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (72, N'Activation and coordination of health personnel in a public health emergency', 27, N'R', N'1.4', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (73, N'Emergency logistic and supply chain management', 27, N'R', N'1.5', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (74, N'Research, development and innovation', 27, N'R', N'1.6', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (75, N'Public health and security authorities (e.g., law enforcement, border control, customs) linked during a suspect or confirmed biological, chemical or radiological event', 28, N'R', N'2.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (76, N'Case management', 29, N'R', N'3.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (77, N'Utilization of EHS', 29, N'R', N'3.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (78, N'Continuity of EHS', 29, N'R', N'3.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (79, N'IPC programmes', 30, N'R', N'4.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (80, N'HCAI surveillance', 30, N'R', N'4.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (81, N'Safe environment in health facilities', 30, N'R', N'4.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (82, N'RCCE systems for emergencies', 31, N'R', N'5.1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (83, N'Risk communication', 31, N'R', N'5.2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (84, N'Community engagement', 31, N'R', N'5.3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (85, N'Core capacity requirements at all times for PoEs', 32, N'PoE', N'1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (86, N'Public health response at PoEs', 32, N'PoE', N'2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (87, N'Risk-based approach to international travel-related measures', 32, N'PoE', N'3', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (88, N'Mechanisms established and functioning for detecting and responding to chemical events or emergencies', 33, N'CE', N'1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (89, N'Enabling environment in place for management of chemical events', 33, N'CE', N'2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (90, N'Mechanisms established and functioning for detecting and responding to radiological and nuclear emergencies', 34, N'RE', N'1', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     INSERT [dbo].[TechnicalAreaIndicators]
--         ([TechnicalAreaIndicatorId], [Name], [TechnicalAreaId], [IndicatorCode], [IndicatorCodeId], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         (91, N'Enabling environment in place for management of radiological and nuclear emergencies', 34, N'RE', N'2', GETUTCDATE(), 0, GETUTCDATE(), 0)
--     SET IDENTITY_INSERT [dbo].[TechnicalAreaIndicators] OFF
-- END

-- IF (NOT EXISTS (SELECT TOP 1
--     1
-- FROM PlanStages))
-- BEGIN

--     INSERT INTO PlanStages
--         (Stage)
--     VALUES
--         ('NotStarted'),
--         ('JustStarted'),
--         ('OnGoing'),
--         ('AdvancedStage'),
--         ('Completed')

-- END

-- IF (NOT EXISTS (SELECT TOP 1
--     1
-- FROM PlanStatuses))
-- BEGIN

--     INSERT INTO PlanStatuses
--         (Status)
--     VALUES
--         ('Cancelled'),
--         ('Active'),
--         ('Draft'),
--         ('Complete')

-- END

-- IF (NOT EXISTS (SELECT TOP 1
--     1
-- FROM PlanTypes))
-- BEGIN

--     INSERT INTO PlanTypes
--         ([Type])
--     VALUES
--         ('Strategic'),
--         ('Operational')

-- END

-- IF (NOT EXISTS (SELECT TOP 1
--     1
-- FROM AssessmentTypes))
-- BEGIN

--     INSERT INTO AssessmentTypes
--         ([Type])
--     VALUES
--         ('JEE'),
--         ('ESPAR')
-- END

-- IF (NOT EXISTS (SELECT TOP 1
--     1
-- FROM CommonIndicators))
-- BEGIN
--     INSERT INTO [dbo].[CommonIndicators]
--         ([IndicatorId], [Name])
--     VALUES
--         (1, 'Policy, legal and normative instruments' ),
--         (2, 'Gender equality in health emergencies'),
--         (3, 'Financing for IHR implementation '),
--         (4, 'Financing for public health emergency response'),
--         (5, 'National IHR Focal Point functions '),
--         (6, 'Multisectoral coordination mechanisms'),
--         (7, 'Advocacy for IHR implementation'),
--         (8, 'Multisectoral coordination on AMR'),
--         (9, 'Surveillance of AMR'),
--         (10, 'Prevention of Multi-Drug Resistant Organism (MDRO)'),
--         (11, 'Optimal use of antimicrobial medicines in human health'),
--         (12, 'Optimal use of antimicrobial medicines in animal health and agriculture'),
--         (13, 'Surveillance of zoonotic diseases'),
--         (14, 'Response to zoonotic diseases'),
--         (15, 'Sanitary animal production practices '),
--         (16, 'Surveillance of foodborne diseases and contamination'),
--         (17, 'Response and management of food safety emergencies'),
--         (18, 'Management and Hygiene Practices in Food Processing'),
--         (19, 'Whole-of-Government biosafety and biosecurity system is in place for human, animal, and agriculture facilities'),
--         (20, 'Biosafety and biosecurity training and practices in all relevant sectors (including human, animal and agriculture)'),
--         (21, 'New and experimental vaccines for epidemics of VPDs'),
--         (22, 'Mass vaccination for epidemics of VPDs'),
--         (23, 'Specimen referral and transport system'),
--         (24, 'Laboratory quality system'),
--         (25, 'Laboratory testing capacity modalities'),
--         (26, 'Effective national diagnostic network'),
--         (27, 'Early warning surveillance function'),
--         (28, 'Event verification and investigation'),
--         (29, 'Analysis and information sharing'),
--         (30, 'Human resources for implementation of IHR'),
--         (31, 'Multisectoral workforce strategy'),
--         (32, 'Workforce training'),
--         (33, 'Workforce surge during a public health event'),
--         (34, 'Emergency risk assessment'),
--         (35, 'Emergency readiness assessment'),
--         (36, 'Planning for health emergencies'),
--         (37, 'Public Health Emergency Operations Centre (PHEOC)'),
--         (38, 'Management of health emergency response'),
--         (39, 'Activation and coordination of health personel in a public health emergency'),
--         (40, 'Emergency logistic and supply chain management'),
--         (41, 'Research, development and innovation'),
--         (42, 'Public Health and Security Authorities, (e.g. Law Enforcement, Border Control, Customs) are linked during a suspect or confirmed biological event'),
--         (43, 'Case management'),
--         (44, 'Utilization of health services'),
--         (45, 'Continuity of essential health services (EHS)'),
--         (46, 'IPC programmes'),
--         (47, 'HCAI surveillance'),
--         (48, 'Safe environment in health facilities'),
--         (49, 'RCCE system for emergencies'),
--         (50, 'Coordination of risk communication and infodemic management is effective'),
--         (51, 'Effective communication with communities and infodemic resilience'),
--         (52, 'Community engagement'),
--         (53, 'Core capacity requirements at all times for PoEs (airports, ports and ground crossings)'),
--         (54, 'Public health response at points of entry'),
--         (55, 'Risk-based approach to international travel-related measures '),
--         (56, 'Mechanisms established and functioning for detecting and responding to chemical events or emergencies'),
--         (57, 'Enabling environment in place for management of chemical events'),
--         (58, 'Mechanisms established and functioning for detecting and responding to radiological and nuclear emergencies'),
--         (59, 'Enabling environment in place for management of radiological and nuclear emergencies'),
--         (60, 'Governance and leadership mechanism for health emergency is in place.'),
--         (61, 'Systematic monitoring and evaluation (M&E) of health security for action is in place')
-- END

-- IF (NOT EXISTS (SELECT TOP 1
--     1
-- FROM CommonIndicatorsMapping))
-- BEGIN
--     INSERT INTO [dbo].[CommonIndicatorsMapping]
--         ([CommonIndicatorId], [IndicatorCode], [IndicatorId], [Type])
--     VALUES
--         (1, 'P', '1.1', 1),
--         (1, 'C', '1.1', 2),
--         (1, NULL, '1.1', 3),
--         (1, 'P', '1.1', 4),
--         (2, 'P', '1.2', 1),
--         (2, 'C', '1.2', 2),
--         (2, 'P', '1.2', 4),
--         (3, 'P', '2.1', 1),
--         (3, 'C', '3.1', 2),
--         (3, NULL, '1.2', 3),
--         (3, 'P', '2.1', 4),
--         (4, 'P', '2.2', 1),
--         (4, 'C', '3.2', 2),
--         (4, NULL, '1.3', 3),
--         (4, 'P', '2.2', 4),
--         (5, 'P', '3.1', 1),
--         (5, 'C', '2.1', 2),
--         (5, NULL, '2.1', 3),
--         (5, 'P', '3.1', 4),
--         (6, 'P', '3.2', 1),
--         (6, 'C', '2.2', 2),
--         (6, NULL, '2.2', 3),
--         (6, 'P', '3.2', 4),
--         (7, 'P', '3.3', 1),
--         (7, 'C', '2.3', 2),
--         (7, 'P', '3.3', 4),
--         (8, 'P', '4.1', 1),
--         (8, NULL, '3.1', 3),
--         (8, 'P', '4.1', 4),
--         (9, 'P', '4.2', 1),
--         (9, NULL, '3.2', 3),
--         (9, 'P', '4.2', 4),      
--         (10, 'P', '4.3', 1),
--         (10, NULL, '3.3', 3),
--         (10, 'P', '4.3', 4),
--         (11, 'P', '4.4', 1),
--         (11, NULL, '3.4', 3),
--         (11, 'P', '4.4', 4),
--         (12, 'P', '4.5', 1),
--         (12, 'P', '4.5', 4),
--         (13, 'P', '5.1', 1),
--         (13, 'C', '12.1', 2),
--         (13, NULL, '4.1', 3),
--         (13, 'P', '5.1', 4),
--         (14, 'P', '5.2', 1),
--         (14, NULL, '4.2', 3),
--         (14, 'P', '5.2', 4),
--         (15, 'P', '5.3', 1),
--         (15, 'P', '5.3', 4),
--         (16, 'P', '6.1', 1),
--         (16, 'C', '13.1', 2),
--         (16, NULL, '5.1', 3),
--         (16, 'P', '6.1', 4),
--         (17, 'P', '6.2', 1),
--         (17, NULL, '5.2', 3),
--         (17, 'P', '6.2', 4),
--         (18, 'P', '6.3', 1),
--         (18, 'P', '6.3', 4),
--         (19, 'P', '7.1', 1),
--         (19, 'C', '4.2', 2),
--         (19, NULL, '8.1', 3),
--         (19, 'P', '7.1', 4),
--         (20, 'P', '7.2', 1),
--         (20, NULL, '8.2', 3),
--         (20, 'P', '7.2', 4),
--         (21, 'P', '8.1', 1),
--         (21, NULL, '6.1', 3),
--         (21, 'P', '8.1', 4),
--         (22, 'P', '8.2', 1),
--         (22, NULL, '6.2', 3),
--         (22, 'P', '8.2', 4),
--         (23, 'D', '1.1', 1),
--         (23, 'C', '4.1', 2),
--         (23, NULL, '7.2', 3),
--         (23, 'D', '1.1', 4),
--         (24, 'D', '1.2', 1),
--         (24, 'C', '4.3', 2),
--         (24, 'D', '1.2', 4),
--         (25, 'D', '1.3', 1),
--         (25, 'C', '4.4', 2),
--         (25, NULL, '7.1', 3),
--         (25, 'D', '1.3', 4),
--         (26, 'D', '1.4', 1),
--         (26, 'C', '4.5', 2),
--         (26, NULL, '7.3', 3),
--         (26, 'D', '1.4', 4),
--         (27, 'D', '2.1', 1),
--         (27, 'C', '5.1', 2),
--         (27, NULL, '9.1', 3),
--         (27, 'D', '2.1', 4),
--         (28, 'D', '2.2', 1),
--         (28, 'C', '5.2', 2),
--         (28, NULL, '9.2', 3),
--         (28, 'D', '2.2', 4),
--         (29, 'D', '2.3', 1),
--         (29, NULL, '9.3', 3),
--         (29, 'D', '2.3', 4),
--         (30, 'D', '3.2', 1),
--         (30, 'C', '6.1', 2),
--         (30, NULL, '10.2', 3),
--         (30, 'D', '3.2', 4),
--         (31, 'D', '3.1', 1),
--         (31, NULL, '10.1', 3),
--         (31, 'D', '3.1', 4),
--         (32, 'D', '3.3', 1),
--         (32, NULL, '10.3', 3),
--         (32, 'D', '3.3', 4),
--         (33, 'D', '3.4', 1),
--         (33, 'C', '6.2', 2),
--         (33, NULL, '10.4', 3),
--         (33, 'D', '3.4', 4),
--         (34, 'R', '1.1', 1),
--         (34, NULL, '20.1', 3),
--         (34, 'R', '1.1', 4),
--         (35, 'R', '1.2', 1),
--         (35, 'C', '7.1', 2),
--         (35, NULL, '11.1', 3),
--         (35, 'R', '1.2', 4),
--         (36, 'R', '1.3', 1),
--         (36, 'C', '7.2', 2),
--         (36, null, '11.2', 3),
--         (36, 'R', '1.3', 4),
--         (37, 'R', '1.4', 1),
--         (37 , null, '12.1', 3),
--         (37, 'R', '1.4', 4),
--         (38, 'R', '1.5', 1),
--         (38, null, '12.2', 3),
--         (38, 'R', '1.5', 4),
--         (39, 'R', '1.6', 1),
--         (39, null, '12.3', 3),
--         (39, 'R', '1.6', 4),
--         (40, 'R', '1.7', 1),
--         (40, 'C', '7.3', 2),
--         (40, null, '21.1', 3),
--         (40, 'R', '1.7', 4),
--         (41, 'R', '1.8', 1),
--         (41, null, '24.1', 3),
--         (41, 'R', '1.8', 4),
--         (42, 'R', '2.1', 1),
--         (42, null, '13.1', 3),
--         (42, 'R', '2.1', 4),
--         (43, 'R', '3.1', 1),
--         (43, 'C', '8.1', 2),
--         (43, null, '14.1', 3),
--         (43, 'R', '3.1', 4),
--         (44, 'R', '3.2', 1),
--         (44, 'C', '8.2', 2),
--         (44, null, '14.2', 3),
--         (44, 'R', '3.2', 4),
--         (45, 'R', '3.3', 1),
--         (45, 'C', '8.3', 2),
--         (45, null, '14.3', 3),
--         (45, 'R', '3.3', 4),
--         (46, 'R', '4.1', 1),
--         (46, 'C', '9.1', 2),
--         (46, 'R', '4.1', 4),
--         (47, 'R', '4.2', 1),
--         (47, 'C', '9.2', 2),
--         (47, 'R', '4.2', 4),
--         (48, 'R', '4.3', 1),
--         (48, 'C', '9.3', 2),
--         (48, 'R', '4.3', 4),
--         (49, 'R', '5.1', 1),
--         (49, 'C', '10.1', 2),
--         (49, null, '15.1', 3),
--         (49, 'R', '5.1', 4),
--         (50, 'R', '5.2', 1),
--         (50, 'C', '10.2', 2),
--         (50, null, '15.2', 3),
--         (50, 'R', '5.2', 4),
--         (51, null, '15.3', 3),
--         (52, 'R', '5.3', 1),
--         (52, 'C', '10.3', 2),
--         (52, null, '22.1', 3),
--         (52, 'R', '5.3', 4),
--         (53, 'PoE', '1', 1),
--         (53, 'C', '11.1', 2),
--         (53, null, '16.1', 3),
--         (53, 'PoE', '1', 4),
--         (54, 'PoE', '2', 1),
--         (54, 'C', '11.2', 2),
--         (54, null, '16.2', 3),
--         (54, 'PoE', '2', 4),
--         (55, 'PoE', '3', 1),
--         (55, 'C', '11.3', 2),
--         (55, 'PoE', '3', 4),
--         (56, 'CE', '1', 1),
--         (56, 'C', '14.1', 2),
--         (56, null, '17.1', 3),
--         (56, 'CE', '1', 4),
--         (57, 'CE', '2', 1),
--         (57, 'CE', '2', 4),
--         (58, 'RE', '1', 1),
--         (58, 'C', '15.1', 2),
--         (58, null, '18.1', 3),
--         (58, 'RE', '1', 4),
--         (59, 'RE', '2', 1),
--         (59, 'RE', '2', 4),
--         (60, null, '19.1', 3),
--         (61, null, '23.1', 3)
-- END

-- IF( NOT EXISTS (SELECT TOP 1
--     1
-- FROM StrategicActionImpacts))
-- BEGIN
--     INSERT INTO StrategicActionImpacts
--         (Impact)
--     VALUES
--         ('High'),
--         ('Medium'),
--         ('Low')

-- END

-- IF( NOT EXISTS (SELECT TOP 1
--     1
-- FROM StrategicActionFeasibility))
-- BEGIN
--     INSERT INTO StrategicActionFeasibility
--         (Feasibility)
--     VALUES
--         ('Easy'),
--         ('Medium'),
--         ('Difficult')

-- END

-- IF( NOT EXISTS (SELECT TOP 1
--     1
-- FROM StrategicActionPriorities))
-- BEGIN
--     INSERT INTO StrategicActionPriorities
--         (Priority)
--     VALUES
--         ('Very High'),
--         ('High'),
--         ('Medium'),
--         ('Low'),
--         ('Very Low')
-- END

-- TRUNCATE TABLE [dbo].[Configurations]

-- INSERT INTO [dbo].[Configurations]
--     ([Key], [Value])
-- VALUES
--     ('TenantId', 'f610c0b7-bd24-4b39-810b-3dc280afb590'),
--     ('ApplicationVersion', '0.0.1'),
--     ('MaxStrategicPlanAllowed', '2'),
--     ('TechnicalAreaExcelColumn', 'A'),
--     ('IndicatorExcelColumn', 'B'),
--     ('ValidExcelExtensions', '.xls,.xlsx'),
--     ('AuthTokenURL', 'https://login.microsoftonline.com/f610c0b7-bd24-4b39-810b-3dc280afb590/oauth2/token'),
--     ('JEEClientId', '98c4c8fc-3833-45fb-89f3-fea84a14595b'),
--     ('JEEClientSecret', 'TDU8Q~KXsNCqOdrCDY4Bzq2z6LP896wmOoB2qcQd'),
--     ('JEEAuthTokenParameterName', 'access_token'),
--     ('JEEScoreUrl', 'https://jeereports-uat.who.int/api/JeeReportsApi/NOK/JeeMissions/scores'),
--     ('JEERecommendationUrl', 'https://jeereports-uat.who.int/api/JeeReportsApi/NOK/JeeMissions/recommendations'),
--     ('SPARClientId', '9038bfe4-467a-460d-90b6-ed54898233cd'),
--     ('SPARClientSecret', 'v4X8Q~VjgKMCDsKitBHzhgYSTwD9clxKZcdLCaWg'),
--     ('SPARScope', 'api://9038bfe4-467a-460d-90b6-ed54898233cd'),
--     ('SPARXAPIKey', '2CB22047-C860-4CE6-91EB-9B084359A909'),
--     ('SPARSubmissionURL', 'Https://extranet.who.int/e-spar/api/GetSubmissionV2')


-- IF NOT EXISTS (SELECT TOP 1
--     1
-- FROM [dbo].[Currencies])
-- BEGIN
--     INSERT INTO [dbo].[Currencies]
--         ([Code], [Sign], [IsDefault], [ConversionFactor], [Description], [CreatedAt], [CreatedBy], [LastUpdatedAt], [LastUpdatedBy])
--     VALUES
--         ('USD', '$', 1, 1, 'United States Dollar', GETUTCDATE(), 1, GETUTCDATE(), 1)
-- END

-- -- Add permissions if not exist.
-- IF NOT EXISTS (SELECT TOP 1
--     1
-- FROM [dbo].[Permissions])
-- BEGIN
--     INSERT INTO [dbo].[Permissions]
--         ([Activity], [MinimalRoleId])
--     VALUES
--         ('InviteUser', 5),
--         ('ApproveUserAccount', 6),
--         ('UpdateUserAccount', 6),
--         ('CreatePlan', 3),
--         ('ViewPlan', 2),
--         ('UpdatePlan', 3),
--         ('DownloadPlan', 3),
--         ('UploadPlan', 3),
--         ('DeletePlan', 3),
--         ('ActivatePlan', 3),
--         ('UpdatePlanVisibility', 5),
--         ('AddOrEditAction', 5),
--         ('ApproveNewAction', 5),
--         ('UpdateStrategicAction', 5),
--         ('UpdateImplementationStatusOfStrategicAction', 3),
--         ('UpdateActionDetails', 4),
--         ('CompleteOrReviewPlan', 5),
--         ('CancelPlan', 5),
--         ('ClonePlan', 3),
--         ('SummaryDashboards', 1),
--         ('ViewOwnCountryActivePlanReport', 3)
-- ENDd