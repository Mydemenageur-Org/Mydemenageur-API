using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Reviews;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.DAL.Models;
using Mydemenageur.BLL.Helpers;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Collections.Generic.IDictionary<string, object>>;


namespace Mydemenageur.BLL.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IDPReview _dpReview;
        private long _reviewCount = 0;
        private readonly IDPMyDemenageurUser _dpUser;
        private readonly IDPCity _dpCity;
        private readonly IDPGrosBras _dpGrosBras;

        private readonly IMapper _mapper;

        public ReviewsService(IDPReview dpReview, IDPMyDemenageurUser dpMyDemenageurUser, IDPCity dPCity, IDPGrosBras dPGrosBras, IMapper mapper)
        {
            _dpUser = dpMyDemenageurUser;
            _dpReview = dpReview;
            _dpCity = dPCity;
            _dpGrosBras = dPGrosBras;

            _mapper = mapper;
        }

        public async Task<Review> GetReview(string id)
        {
            Review review = await _dpReview.Obtain().Where(x => x.Id == id).FirstOrDefaultAsync();

            return review;
        }

        public async Task<IList<ReviewAllopulated>> GetAllReviews(int pageNumber = -1, int numberOfElementsPerPage = -1)
        {
            List<ReviewAllopulated> reviews = new List<ReviewAllopulated>();
            City city = new City();

            var cursor = _dpReview.GetCollection().Find(new BsonDocument())
                    .SortByDescending(review => review.CreatedAt);

            if (pageNumber >= 0 && numberOfElementsPerPage > 0)
            {
                cursor.Limit(numberOfElementsPerPage).Skip(pageNumber * numberOfElementsPerPage);
            }

            cursor.ToListAsync().Result.ForEach((review) => {
                var myDem = _dpUser.GetUserById(review.Deposer).FirstOrDefault();
                var reciever = _dpUser.GetUserById(review.Receiver).FirstOrDefault();
                var profilReceiver = _dpGrosBras.Obtain().Where(w => w.MyDemenageurUserId == reciever.Id).FirstOrDefault();
                if (profilReceiver != null)
                {
                    city = _dpCity.GetCityById(profilReceiver.CityId).FirstOrDefault();
                } else
                {
                    city.CreatedAt = DateTime.Now;
                    city.Label = "pas-de-ville";
                    profilReceiver = new GrosBras();
                }

                GrosBrasPopulated grosBras = _mapper.Map<GrosBrasPopulated>(profilReceiver);
                grosBras.MyDemenageurUser = myDem;
                grosBras.City = city;

                ReviewAllopulated reviewsPopulated = new ReviewAllopulated
                {
                    Id = review.Id,
                    Deposer = myDem,
                    ReceiverProfil = grosBras,
                    Receiver = reciever,
                    Note = review.Note,
                    Description = review.Description,
                    CreatedAt = review.CreatedAt,
                    UpdatedAt = review.UpdatedAt,
                    Commentaires = review.Commentaires
                };
                reviews.Add(reviewsPopulated);
            });
            return reviews;
        }

        public async Task<IList<ReviewPopulated>> GetReviews(string id)
        {
            List<ReviewPopulated> reviews = new List<ReviewPopulated>();

            var cursor = _dpReview.Obtain().Where(x => x.Receiver == id);

            cursor.ToListAsync().Result.ForEach((review) => {
                var myDem = _dpUser.GetUserById(review.Deposer).FirstOrDefault();
                ReviewPopulated reviewsPopulated = new ReviewPopulated
                {
                    Id = review.Id,
                    Deposer = myDem,
                    Receiver = review.Receiver,
                    Note = review.Note,
                    Description = review.Description,
                    CreatedAt = review.CreatedAt,
                    UpdatedAt = review.UpdatedAt,
                    Commentaires = review.Commentaires
                };
                reviews.Add(reviewsPopulated);
            });
            return reviews;
        }
        
        public async Task<IList<ReviewPopulated>> GetReviewsBetween(string receiver, string deposer)
        {
            List<ReviewPopulated> reviews = new List<ReviewPopulated>();

            var cursor = _dpReview.Obtain().Where(x => x.Receiver == receiver && x.Deposer == deposer);

            cursor.ToListAsync().Result.ForEach((review) => {
                var myDem = _dpUser.GetUserById(review.Deposer).FirstOrDefault();
                ReviewPopulated reviewsPopulated = new ReviewPopulated
                {
                    Id = review.Id,
                    Deposer = myDem,
                    Receiver = review.Receiver,
                    Note = review.Note,
                    Description = review.Description,
                    CreatedAt = review.CreatedAt,
                    UpdatedAt = review.UpdatedAt,
                    Commentaires = review.Commentaires
                };
                reviews.Add(reviewsPopulated);
            });
            return reviews;
        }
        
        public long CountReviews()
        {
            long reviewsNumber = _dpReview.Obtain().ToList().Count();
            _reviewCount = reviewsNumber;
            return reviewsNumber;
        }

        public async Task<Review> CreateReview(Review review)
        {

            await _dpReview.GetCollection().InsertOneAsync(review);
            GrosBras grosBras = _dpGrosBras.GetCollection().Find(w => w.MyDemenageurUserId == review.Receiver).FirstOrDefault();
            if (grosBras == null)
            {
                throw new Exception("GrosBras not found");
            }

            ReviewGradeUpdater(null, review.Note, grosBras);

            return review;
        }

        public async Task<string> UpdateReview(ReviewUpdater reviewUpdater)
        {
            Review review = new Review();
            review.Id = reviewUpdater.Id;
            review.Commentaires = reviewUpdater.Commentaires;
            review.Deposer = reviewUpdater.Deposer;
            review.Receiver = reviewUpdater.Receiver;
            review.Note = reviewUpdater.Note;
            review.Description = reviewUpdater.Description;
            review.CreatedAt = reviewUpdater.CreatedAt;
            review.UpdatedAt = reviewUpdater.UpdatedAt;
            
            await _dpReview.GetCollection().ReplaceOneAsync(
                dpReview => dpReview.Id == review.Id,
                review
            );
            
            GrosBras grosBras = _dpGrosBras.GetCollection().Find(w => w.MyDemenageurUserId == review.Receiver).FirstOrDefault();
            if (grosBras == null)
            {
                throw new Exception("GrosBras not found");
            }

            ReviewGradeUpdater(reviewUpdater.LastNote, reviewUpdater.Note, grosBras);

            return review.Id;
        }

        public async Task<string> DeleteReview(string id)
        {
            return null;
        }

        public async Task<List<ReviewAllopulated>> GetReviewsFromUser(string mdUserId, bool count)
        {
            List<ReviewAllopulated> reviews = new List<ReviewAllopulated>();
            City city = new City();

            var cursor = _dpReview.GetCollection().Find(new BsonDocument())
                    .SortByDescending(review => review.CreatedAt);

            cursor.ToListAsync().Result.ForEach((review) => {
                if (review.Receiver == mdUserId)
                {
                    var myDem = _dpUser.GetUserById(review.Deposer).FirstOrDefault();
                    var reciever = _dpUser.GetUserById(review.Receiver).FirstOrDefault();
                    var profilReceiver = _dpGrosBras.Obtain().Where(w => w.MyDemenageurUserId == reciever.Id).FirstOrDefault();
                    if (profilReceiver != null)
                    {
                        city = _dpCity.GetCityById(profilReceiver.CityId).FirstOrDefault();
                    }
                    else
                    {
                        city.CreatedAt = DateTime.Now;
                        city.Label = "pas-de-ville";
                        profilReceiver = new GrosBras();
                    }

                    GrosBrasPopulated grosBras = _mapper.Map<GrosBrasPopulated>(profilReceiver);
                    grosBras.MyDemenageurUser = myDem;
                    grosBras.City = city;

                    ReviewAllopulated reviewsPopulated = new ReviewAllopulated
                    {
                        Id = review.Id,
                        Deposer = myDem,
                        ReceiverProfil = grosBras,
                        Receiver = reciever,
                        Note = review.Note,
                        Description = review.Description,
                        CreatedAt = review.CreatedAt,
                        UpdatedAt = review.UpdatedAt,
                        Commentaires = review.Commentaires
                    };
                    reviews.Add(reviewsPopulated);
                }
            });
            return reviews;
        }

        public async Task<SchemaReview> GetSchemasStat()
        {
            SchemaReview schema = new SchemaReview();

            long statRate = _dpReview.Obtain().Where(w => w.Note == "3").ToList().Count();

            if (_reviewCount == 0)
            {
                _reviewCount = _dpReview.Obtain().ToList().Count();
            }

            schema.ReviewNumber = _reviewCount;
            schema.ReviewRate = (statRate * 100 / _reviewCount);

            return schema;
        }

        public async Task<SchemaReview> GetSchemaStatFromUser(string id)
        {
            SchemaReview schemaUser = new SchemaReview();
            if (_reviewCount == 0)
            {
                _reviewCount = _dpReview.Obtain().ToList().Count();
            }

            string veryGoodGrade = (await _dpReview.Obtain().Where(x => x.Receiver == id).Where(w => w.Note == "3").CountAsync() * 100 / _reviewCount).ToString();
            string goodGrade = (await _dpReview.Obtain().Where(x => x.Receiver == id).Where(w => w.Note == "2").CountAsync() * 100 / _reviewCount).ToString();
            string mediumGrade = (await _dpReview.Obtain().Where(x => x.Receiver == id).Where(w => w.Note == "1").CountAsync() * 100 / _reviewCount).ToString();
            string badGrade = (await _dpReview.Obtain().Where(x => x.Receiver == id).Where(w => w.Note == "0").CountAsync() * 100 / _reviewCount).ToString();

            schemaUser.VeryGoodReview = veryGoodGrade;
            schemaUser.GoodReview = goodGrade;
            schemaUser.MediumReview = mediumGrade;
            schemaUser.BadReview = badGrade;

            return schemaUser;
        }
        private void ReviewGradeUpdater(string lastNote, string note, GrosBras grosBras)
        {
            if (!string.IsNullOrWhiteSpace(lastNote))
            {
                Dictionary<string, TFunc> actionsDecr = new Dictionary<string, TFunc>() {
                    {"3", (input) => {
                            grosBras.VeryGoodGrade = grosBras.VeryGoodGrade - 1;
                            return null; 
                        } 
                    },
                    {"2", (input) => {
                            grosBras.GoodGrade = grosBras.GoodGrade - 1;
                            return null; 
                        } 
                    },
                    {"1", (input) => {
                        grosBras.MediumGrade = grosBras.MediumGrade - 1;
                        return null; } },
                    {"0", (input) => {
                        grosBras.BadGrade = grosBras.BadGrade - 1;
                        return null; } },
                };

                var outputDecr = actionsDecr[lastNote](null);
            }
            Dictionary<string, TFunc> actions = new Dictionary<string, TFunc>() {
                {"3", (input) => {
                    grosBras.VeryGoodGrade = grosBras.VeryGoodGrade + 1;
                    return null; 
                    } 
                },
                {"2", (input) => {
                    grosBras.GoodGrade = grosBras.GoodGrade + 1;
                    return null; 
                    } 
                },
                {"1", (input) => {
                    grosBras.MediumGrade = grosBras.MediumGrade + 1;
                    return null; } },
                {"0", (input) => {
                    grosBras.BadGrade = grosBras.BadGrade + 1;
                    return null; } },
            };

            var output = actions[note](null);

            _dpGrosBras.GetCollection().ReplaceOne(w => w.Id == grosBras.Id, grosBras);
        }
    }
}