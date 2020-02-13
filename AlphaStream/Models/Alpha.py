from datetime import datetime
from .Author import Author
from .Project import Project


class Alpha(object):
    """Algorithm alpha model from the Alpha Streams marketplace."""

    def __init__(self, json):

        self.Id = json['id']

        self.Authors = []
        authors = json.get('authors', None)
        if authors is not None:
            for a in json['authors']:
                self.Authors.append(Author(a))

        self.AssetClasses = json.get('asset-classes', None)

        self.Accuracy = json.get('accuracy', None)

        self.AnalysesPerformed = json.get('analyses-performed', None)

        self.AuthorTrading = json.get('author-trading', False)

        self.Description = json.get('description', '')

        self.EstimatedDepth = json.get('estimated-depth', None)

        self.ExclusiveAvailable = json.get('exclusive-available', None)

        self.ExclusiveSubscriptionFee = json.get('exclusive-subscription-fee', None)

        self.EstimatedEffort = json.get('estimated-effort', None)

        self.ListedTime = datetime.utcfromtimestamp(json['listed-time']) if 'listed-time' in json else None

        self.Name = json.get('name', None)

        self.Project = Project(json.get('project', None))

        self.Uniqueness = json.get('uniqueness', None)

        self.SharpeRatio = json.get('sharpe-ratio', None)

        self.SharedSubscriptionFee = json.get('subscription-fee', None)

        self.Version = json.get('version', None)

        self.Status = json.get('status', None)
        
        self.InSampleInsights = json.get('in-sample-insights', None)

        self.LiveTradingInsights = json.get('live-trading-insights', None)

        self.OutOfSampleInsights = json.get('out-of-sample-insights', None)

        self.Tags = json.get('tags', [])

        self.Parameters = json.get('parameters', None)

        self.OutOfSampleDtwDistance = json.get('out-of-sample-dtw-distance', None)

        self.OutOfSampleReturnsCorrelation = json.get('out-of-sample-returns-correlation', None)

        self.Trial = json.get('trial', 0)

    def __repr__(self):
        return f'''
Alpha Id: {self.Id}
    Project: {self.Project.Name}
    Sharpe Ratio: {self.SharpeRatio}
    Uniqueness: {self.Uniqueness}
    Exclusive Available: {self.ExclusiveAvailable}
    Listed: {self.ListedTime}
    Status: {self.Status}'''
